using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RMM.Data;
using RMM.Data.Model;

namespace RMM.Business.OptionService
{
    public class OptionService : IOptionService
    {
        private RmmDataContext datacontext = null;

        public Result<Option> GetOption()
        {
            return Result<Option>.SafeExecute<IOptionService>(result =>
            {

                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    var option = (from t in datacontext.Option
                                  select t).First();

                    result.Value = option;
                }

            }, () => "erreur");
        }

        public Result<Option> UpdateOption(Option optionToUpdate)
        {
            return Result<Option>.SafeExecute<IOptionService>(result =>
            {

                var entityToUpdate = datacontext.Option.First();

                entityToUpdate.IsComparator = optionToUpdate.IsComparator;
                entityToUpdate.IsPassword = optionToUpdate.IsPassword;
                entityToUpdate.IsPrimaryTile = optionToUpdate.IsPrimaryTile;
                entityToUpdate.IsReport = optionToUpdate.IsReport;
                entityToUpdate.IsSynchro = optionToUpdate.IsSynchro;

                datacontext.SubmitChanges();

                result.Value = entityToUpdate;

            }, () => "erreur");


        }


        public Result<Option> SetFirstTimeOption()
        {
            return Result<Option>.SafeExecute<IOptionService>(result =>
            {
                var newFirstTimeOption = new Option()
                {
                    IsComparator = false,
                    IsPrimaryTile = false,
                    IsPassword = false,
                    IsReport = false,
                    IsSynchro = false,
                    ModifiedDate = DateTime.Now
                };

                datacontext.Option.InsertOnSubmit(newFirstTimeOption);

                datacontext.SubmitChanges();

                result.Value = datacontext.Option.First();
            }, () => "erreur");


        }
    }
}
