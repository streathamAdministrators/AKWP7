using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMM.Business.OptionService
{
    public interface IOptionService
    {
        //Retournera une liste de Dto
        Result<OptionDto> GetOption();

        //Passage d'un dto en param apres
        Result<OptionDto> UpdateOption(OptionDto optionToUpdate);
    }
}
