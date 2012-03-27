using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RMM.Data.Model;

namespace RMM.Business.TransactionService
{
    public interface ITransactionService
    {

        Result<Transaction> DeleteTransactionById(int transactionId);


        Result<Transaction> GetTransactionById(int transactionId, bool OnMinimal);


        Result<List<Transaction>> GetTransactionsByCategoryId(int categoryId, bool OnMinimal);


        Result<List<Transaction>> GetTransactionsByAccountId(int accountId, bool OnMinimal);


        Result<Transaction> CreateTransaction(CreateTransactionCommand nouvelleTransaction);


        Result<Transaction> UpdateTransaction(EditTransactionCommand EditTransactionCommand);

        Result<List<Transaction>> GetAllTransactions(bool OnMinimal);
        
    }
}
