using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMM.Business.TransactionService
{
    public interface ITransactionService
    {
        //Renvoi l'element supprimé
        Result<TransactionDto> DeleteTransactionById(int transactionId);

        //Retournera un Dto
        Result<TransactionDto> GetTransactionById(int transactionId);

        //Retournera une liste de Dto
        Result<List<TransactionDto>> GetTransactionsByCategoryId(int categoryId);

        //Retournera une liste de Dto
        Result<List<TransactionDto>> GetTransactionsByAccountId(int accountId);

        //Passage d'un dto en param apres
        Result<TransactionDto> CreateTransaction(TransactionDto transaction);

        //Passage d'un dto en param apres
        Result<TransactionDto> UpdateTransaction(TransactionDto transactionToUpdate);

        Result<List<TransactionDto>> GetAllTransactions();
        
    }
}
