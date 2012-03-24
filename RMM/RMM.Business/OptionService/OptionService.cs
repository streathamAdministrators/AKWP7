using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RMM.Data;
using RMM.Business.ExtensionMethods;

namespace RMM.Business.OptionService
{
    public class OptionService : IOptionService
    {
        private RmmDataContext datacontext = null;

        public Result<OptionDto> GetOption()
        {
            return Result<OptionDto>.SafeExecute<IOptionService>(result =>
            {

                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    var option = (from t in datacontext.Option
                                       select t).First();



                    //MAPPING de option a optionDto
                    var dto = option.ToOptionDto();


                    result.Value = dto;
                }

            }, () => "erreur");
        }

        public Result<OptionDto> UpdateOption(OptionDto optionToUpdate)
        {
            return Result<OptionDto>.SafeExecute<IOptionService>(result =>
            {
                //MAPPING
                var UpdatedEntity = optionToUpdate.ToOptionEntity();



                var entityToUpdate = datacontext.Option.First();

                entityToUpdate.IsComparator = UpdatedEntity.IsComparator;
                entityToUpdate.IsPassword = UpdatedEntity.IsPassword;
                entityToUpdate.IsPrimaryTile = UpdatedEntity.IsPrimaryTile;
                entityToUpdate.IsReport = UpdatedEntity.IsReport;
                entityToUpdate.IsSynchro = UpdatedEntity.IsSynchro;

                datacontext.SubmitChanges();

                result.Value = optionToUpdate;

            }, () => "erreur");

            
        }


        public Result<OptionDto> SetFirstTimeOption()
        {
            return Result<OptionDto>.SafeExecute<IOptionService>(result =>
            {
                var newFirstTimeOptionDto = new OptionDto()
                {
                    IsComparator = false,
                    IsPrimaryTile = false,
                    IsPassword = false,
                    Isreport = false,
                    IsSynchro = false
                };

                var newFirstEntity = newFirstTimeOptionDto.ToOptionEntity();

                datacontext.Option.InsertOnSubmit(newFirstEntity);

                datacontext.SubmitChanges();

                var AddedOption = datacontext.Option.First();

                result.Value = AddedOption.ToOptionDto();

            }, () => "erreur");

            
        }
    }
}
