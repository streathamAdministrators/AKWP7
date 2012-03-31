using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RMM.Data.Model;

namespace RMM.Business.OptionService
{
    public interface IOptionService
    {
        /// <summary>
        /// Retourne un Object de type Option permettant de recuperer, de la base, les options.
        /// </summary>
        /// <returns>retourne un object option</returns>
        Result<Option> GetOption();

        /// <summary>
        /// recuperation du compte Favori. Le compte étant favori possede son Id dans la propriétée favori des options de l'application 
        /// </summary>
        /// <returns>retourne l'Id du compte</returns>
        Result<int> GetFavoriteIdAccount();

        /// <summary>
        /// Met à jour le compte Favori. Le compte étant favori possede son Id dans la propriétée favori des options de l'application 
        /// </summary>
        /// <param name="AccountId">Id du compte a Updater</param>
       /// <returns>retourne l'Id du compte</returns>
        Result<int> SetFavoriteIdAccount(int AccountId);

        /// <summary>
        /// Mise a jour des options. Object entier.
        /// </summary>
        /// <param name="optionToUpdate"></param>
        /// <returns>retourne les options updated.</returns>
        Result<Option> UpdateOption(Option optionToUpdate);

        /// <summary>
        /// Set les Options par default sur l'application. 
        /// Attention !  le compte par default sur le site est le numero 1.
        /// </summary>
        /// <returns>retourne l'object option crée</returns>
        Result<Option> SetFirstTimeOption();
    }
}
