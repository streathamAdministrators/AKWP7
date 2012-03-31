using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RMM.Data.Model;
using System.Linq.Expressions;

namespace RMM.Business.AccountService
{
    public interface IAccountService
    {

        /// <summary>
        /// Recupere un compte par son Id. OnMinimal de type bool permet le eagger loading. si OnMinimal est a true,
        /// l'object sera dit "Minimal", le compte sera donc en LazyLoad, est ne possedera pas la liste de transactions.
        /// </summary>
        /// <param name="categoryId">Id de la category</param>
        /// <param name="OnMinimal">Chargement de la liste des transactions pour la category en question</param>
        /// <returns>la category recherchée</returns>
        Result<Account> GetAccountById(int accountId, bool OnMinimal);


        /// <summary>
        /// Recupere TOUS les comptes par son Id. OnMinimal de type bool permet le eagger loading. si OnMinimal est a true,
        /// l'object sera dit "Minimal", les comptes seront donc en LazyLoad, est ne possederont pas leur liste de transactions.
        /// </summary>
        /// <param name="AccountId">Id de la category</param>
        /// <param name="OnMinimal">Chargement de la liste des transactions par category</param>
        /// <returns>liste des categories</returns>
        Result<List<Account>> GetAllAccounts(bool OnMinimal);



        /// <summary>
        /// Creer un compte par la command CreateAccount command.
        /// </summary>
        /// <param name="newAccommand">commande permettant de faire la creation</param>
        /// <returns>Retour de l'objet crée</returns>
        Result<Account> CreateAccount(CreateAccountCommand newAccountCommand);


        /// <summary>
        /// Mise a jour du compte en question.
        /// </summary>
        /// <param name="editAccountCommand">commande permettant de faire l'update</param>
        /// <returns>Retourne l'object mis à jour</returns>
        Result<Account> UpdateAccount(EditAccountCommand editAccountCommand);


        /// <summary>
        /// suppression du compte
        /// </summary>
        /// <param name="accountId">Category à supprimer</param>
        /// <returns>Retourne la catégory supprimée pour un eventuel Undo..</returns>
        Result<Account> DeleteAccountById(int accountId);

    }
}
