using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RMM.Data.Model;

namespace RMM.Business.OptionService
{
    public interface IOptionService
    {
        //Retournera une liste de Dto
        Result<Option> GetOption();

        Result<int> GetFavoriteIdAccount();
        Result<int> SetFavoriteIdAccount(int AccountId);

        //Passage d'un dto en param apres
        Result<Option> UpdateOption(Option optionToUpdate);

        Result<Option> SetFirstTimeOption();
    }
}
