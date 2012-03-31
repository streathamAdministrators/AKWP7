using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RMM.Data.Model;

namespace RMM.Business.TransactionService
{
    public interface ITransactionService
    {
        /// <summary>
        /// Recupere de la base une transaction par son Id. OnMinimal de type bool permet le eagger loading. si OnMinimal est a true,
        /// l'object sera dit "Minimal", la transaction sera donc en LazyLoad
        /// </summary>
        /// <param name="transactionId">Id de la transaction</param>
        /// <param name="OnMinimal">Minimal = true => Lazy sur transaction</param>
        /// <returns>retourne une transaction</returns>
        Result<Transaction> GetTransactionById(int transactionId, bool OnMinimal);

        /// <summary>
        /// Recupere de la base TOUTES les transactions pour une certaine category.  OnMinimal de type bool permet le eagger loading. si OnMinimal est a true,
        /// l'object sera dit "Minimal", les transactions seront donc en LazyLoad, est ne possederont pas leur comptes et categories.
        /// </summary>
        /// <param name="categoryId">Id de la category en question</param>
        /// <param name="OnMinimal">Minimal = true => Lazy sur transaction</param>
        /// <returns></returns>
        Result<List<Transaction>> GetTransactionsByCategoryId(int categoryId, bool OnMinimal);


        /// <summary>
        /// Recupere de la base TOUTES les transactions pour un certain compte.  OnMinimal de type bool permet le eagger loading. si OnMinimal est a true,
        /// l'object sera dit "Minimal", les transactions seront donc en LazyLoad, est ne possederont pas leur comptes et categories.
        /// </summary>
        /// <param name="accountId">Id du compte en question</param>
        /// <param name="OnMinimal">Minimal = true => Lazy sur transaction</param>
        /// <returns></returns>
        Result<List<Transaction>> GetTransactionsByAccountId(int accountId, bool OnMinimal);


        /// <summary>
        /// Recupere toutes les transactions. Non utile dans le projet lui meme mais sert dans les tests. OnMinimal de type bool permet le eagger loading. si OnMinimal est a true,
        /// l'object sera dit "Minimal", les transactions seront donc en LazyLoad, est ne possederont pas leur comptes et categories.
        /// </summary>
        /// <param name="OnMinimal">Minimal = true => Lazy sur transaction</param>
        /// <returns></returns>
        Result<List<Transaction>> GetAllTransactions(bool OnMinimal);


        /// <summary>
        /// Creer une transaction via l'object commande CreateTransactionCommand. Allegement du code avec aucun Id par exemple.
        /// </summary>
        /// <param name="nouvelleTransaction">De type CreateTransaction Command</param>
        /// <returns>retourne la transaction inseré</returns>
        Result<Transaction> CreateTransaction(CreateTransactionCommand nouvelleTransaction);

        /// <summary>
        /// Mise a Jour de la transaction. l'entité est auto daté.
        /// </summary>
        /// <param name="EditTransactionCommand">De Type EditTransactionCommand Command</param>
        /// <returns>Retourne la transaction éditée</returns>
        Result<Transaction> UpdateTransaction(EditTransactionCommand EditTransactionCommand);

        /// <summary>
        /// Supprime la transaction par son Id.
        /// </summary>
        /// <param name="transactionId">Id de la Transaction</param>
        /// <returns>retourne la transaction supprime, pour un eventuel Undo...</returns>
        Result<Transaction> DeleteTransactionById(int transactionId);

        /// <summary>
        /// Supprime TOUTES les transactions pour un compte donné.
        /// </summary>
        /// <param name="accountId">Id du compte en question</param>
        /// <returns>retour de la liste des transactions supprimées pour un eventuel Undo...</returns>
        Result<List<Transaction>> DeleteTransactionsByAccountId(int accountId);

        /// <summary>
        /// Supprime TOUTES les transactions pour une categorie donnée.
        /// </summary>
        /// <param name="categoryId">Id de la catégorie en question</param>
        /// <returns>retour de la liste des transactions supprimées pour un eventuel Undo...</returns>
        Result<List<Transaction>> DeleteTransactionsByCategoryId(int categoryId); 

        
    }
}
