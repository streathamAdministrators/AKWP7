using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RMM.Data;
using RMM.Data.Model;
using System.Data.Linq;
using RMM.Business.Helpers;
using System.Linq.Expressions;


namespace RMM.Business.TransactionService
{
    public class TransactionService : ITransactionService
    {
        private RmmDataContext datacontext = null;

        public Result<Transaction> DeleteTransactionById(int transactionDtoId)
        {
            return Result<Transaction>.SafeExecute<TransactionService>(result =>
                {

                    using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                    {
                        var transaction = (from t in datacontext.Transaction
                                           where t.ID == transactionDtoId
                                           select t).First();

                        if (transaction != null)
                        {
                            datacontext.Transaction.DeleteOnSubmit(transaction);
                            datacontext.SubmitChanges();
                        }


                        result.Value = transaction;
                    }

                }, () => "erreur");
        }

        public Result<Transaction> GetTransactionById(int transactionId, bool OnMinimal)
        {
            return Result<Transaction>.SafeExecute<TransactionService>(result =>
                {
                    using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                    {
                        if (!OnMinimal)
                            datacontext.LoadOptions = DBHelpers.GetConfigurationLoader<Transaction>(t => t.Category, t => t.Account);

                        var transaction = datacontext.Transaction.Where(t => t.ID == transactionId).First();


                        result.Value = transaction;
                    }
                }, () => "erreur");
        }

        public Result<List<Transaction>> GetTransactionsByCategoryId(int categoryId, bool OnMinimal)
        {
            return Result<List<Transaction>>.SafeExecute<TransactionService>(result =>
            {
                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {

                    if (!OnMinimal)
                        datacontext.LoadOptions = DBHelpers.GetConfigurationLoader<Transaction>(t => t.Account, t => t.Category);


                    var transactions = (from t in datacontext.Transaction
                                        where t.Category.ID == categoryId
                                        select t).ToList();


                    result.Value = transactions;
                }
            }, () => "erreur");
        }

        public Result<List<Transaction>> GetTransactionsByAccountId(int accountId, bool OnMinimal)
        {
            return Result<List<Transaction>>.SafeExecute<TransactionService>(result =>
              {
                  using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                  {
                      if (!OnMinimal)
                          datacontext.LoadOptions = DBHelpers.GetConfigurationLoader<Transaction>(t => t.Category, t => t.Account);

                      var transactions = datacontext.Transaction.Where(t => t.Account.ID == accountId).ToList();

                      result.Value = transactions;
                  }
              }, () => "erreur");
        }

        public Result<Transaction> CreateTransaction(CreateTransactionCommand newTransactionCommand)
        {
            return Result<Transaction>.SafeExecute<TransactionService>(result =>
            {
                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    var transaction = new Transaction();

                    Category attachedCategoryEntity;
                    Account attachedAccountEntity;

                    if (newTransactionCommand.categoryId.HasValue)
                    {
                        attachedCategoryEntity = datacontext.Category.Where(c => c.ID == newTransactionCommand.categoryId).First();
                        transaction.Category = attachedCategoryEntity;
                    }

                    if (newTransactionCommand.accountId != 0)
                    {
                        attachedAccountEntity = datacontext.Account.Where(c => c.ID == newTransactionCommand.accountId).First();
                        transaction.Account = attachedAccountEntity;
                    }

                    transaction.Amount = newTransactionCommand.Amount;
                    transaction.Description = newTransactionCommand.Description;
                    transaction.Name = newTransactionCommand.Name;
                    transaction.CreatedDate = DateTime.Now;

                    datacontext.Transaction.InsertOnSubmit(transaction);
                    datacontext.SubmitChanges();

                    var AddedTransac = datacontext.Transaction.Where(a => a.CreatedDate == transaction.CreatedDate).First();

                    result.Value = AddedTransac;

                }
            }, () => "erreur");


        }

        public Result<Transaction> UpdateTransaction(EditTransactionCommand EditTransactionCommand)
        {
            return Result<Transaction>.SafeExecute<TransactionService>(result =>
            {
                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {

                    DataLoadOptions options = new DataLoadOptions();
                    options.LoadWith<Transaction>(c => c.Category);
                    options.LoadWith<Transaction>(c => c.Account);

                    datacontext.LoadOptions = options;


                    var entityToUpdate = datacontext.Transaction.Where(t => t.ID == EditTransactionCommand.id).First();


                    entityToUpdate.Name = EditTransactionCommand.Name;

                    if (entityToUpdate.Category.ID != EditTransactionCommand.categoryId)
                    {
                        var categoryAttachedByUpdate = datacontext.Category.Where(c => c.ID == EditTransactionCommand.categoryId).First();
                        entityToUpdate.Category = categoryAttachedByUpdate;
                    }


                    if (entityToUpdate.Account.ID != EditTransactionCommand.accountId)
                    {
                        var accountAttachedByUpdate = datacontext.Account.Where(c => c.ID == EditTransactionCommand.accountId).First();
                        entityToUpdate.Account = accountAttachedByUpdate;
                    }

                    if (string.IsNullOrEmpty(EditTransactionCommand.Description))
                        entityToUpdate.Description = EditTransactionCommand.Description;

                    entityToUpdate.Amount = EditTransactionCommand.Amount;

                    entityToUpdate.CreatedDate = DateTime.Now;

                    datacontext.SubmitChanges();

                    result.Value = entityToUpdate;

                }
            }, () => "erreur");


        }

        public Result<List<Transaction>> GetAllTransactions(bool OnMinimal)
        {
            return Result<List<Transaction>>.SafeExecute<TransactionService>(result =>
            {
                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    if (!OnMinimal)
                        datacontext.LoadOptions = DBHelpers.GetConfigurationLoader<Transaction>(t => t.Account, t => t.Category);

                    var query = datacontext.Transaction.ToList();

                    result.Value = query;
                }
            }, () => "erreur");
        }

        public Result<List<Transaction>> DeleteTransactionsByAccountId(int accountId)
        {
            return Result<List<Transaction>>.SafeExecute<TransactionService>(result =>
            {
                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    var transaction = datacontext.Transaction.Where(t => t.AccountID == accountId).ToList();

                    if (transaction != null)
                    {
                        datacontext.Transaction.DeleteAllOnSubmit(transaction);
                        datacontext.SubmitChanges();
                    }


                    result.Value = transaction;
                }
            }, () => "erreur");
        }


        public Result<List<Transaction>> DeleteTransactionsByCategoryId(int categoryId)
        {
            return Result<List<Transaction>>.SafeExecute<TransactionService>(result =>
            {
             using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    var transaction = datacontext.Transaction.Where(t => t.CategoryID == categoryId).ToList();

                    if (transaction != null)
                    {
                        datacontext.Transaction.DeleteAllOnSubmit(transaction);
                        datacontext.SubmitChanges();
                    }


                    result.Value = transaction;
                }
            }, () => "erreur");
        }
    }
}

