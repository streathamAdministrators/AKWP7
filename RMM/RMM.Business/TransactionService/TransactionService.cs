using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RMM.Data;
using RMM.Data.Model;
using RMM.Business.ExtensionMethods;
using System.Data.Linq;


namespace RMM.Business.TransactionService
{
    public class TransactionService : ITransactionService
    {
        private RmmDataContext datacontext = null;

        public Result<TransactionDto> DeleteTransactionById(int transactionDtoId)
        {
          return Result<TransactionDto>.SafeExecute<TransactionService>(result =>
                {

                    using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                    {
                        var transaction = (from t in datacontext.Transaction
                                           where t.transactionid == transactionDtoId
                                           select t).First();
                       
                        if (transaction != null)
                        {
                            datacontext.Transaction.DeleteOnSubmit(transaction);
                            datacontext.SubmitChanges();
                        }
                      
                      
                        //MAPPING de transaction a transactionDto
                        var dto = transaction.ToTransactionDto();
                            result.Value = dto;
                    }

                }, () => "erreur");
        }



        public Result<TransactionDto> GetTransactionById(int transactionId)
        {
            return Result<TransactionDto>.SafeExecute<TransactionService>(result =>
                {
                    using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                    {
                        var transaction = (from t in datacontext.Transaction
                                           where t.transactionid == transactionId
                                           select t).First();

                        var dto = transaction.ToTransactionDto();
                            result.Value = dto;
                    }
                }, () => "erreur");
        }

        public Result<List<TransactionDto>> GetTransactionsByCategoryId(int categoryId)
        {
            return Result<List<TransactionDto>>.SafeExecute<TransactionService>(result =>
            {
                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    DataLoadOptions options = new DataLoadOptions();
                    options.LoadWith<Transaction>(c => c.Category);

                    datacontext.LoadOptions = options;

                    var transactions = (from t in datacontext.Transaction
                                       where t.Category.id == categoryId
                                       select t).ToList();

                    
                    var listeDeTransactionVerifiee = new List<TransactionDto>();


                    transactions.ForEach(transItem =>
                        {
                            var dto = transItem.ToTransactionDto();
                                listeDeTransactionVerifiee.Add(dto);
                        });

                    result.Value = listeDeTransactionVerifiee;
                }
            }, () => "erreur");
        }

        public Result<List<TransactionDto>> GetTransactionsByAccountId(int accountId)
        {
          return Result<List<TransactionDto>>.SafeExecute<TransactionService>(result =>
            {
                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    DataLoadOptions options = new DataLoadOptions();
                    options.LoadWith<Transaction>(c => c.Account);

                    datacontext.LoadOptions = options;

                    var transactions = (from t in datacontext.Transaction
                                       where t.Account.id == accountId
                                       select t).ToList();

                    
                    var listeDeTransactionVerifiee = new List<TransactionDto>();


                    transactions.ForEach(transItem =>
                        {
                            var dto = transItem.ToTransactionDto();
                                listeDeTransactionVerifiee.Add(dto);
                        });

                    result.Value = listeDeTransactionVerifiee;
                }
            }, () => "erreur");
        }


        public Result<TransactionDto> CreateTransaction(TransactionDto transaction)
        {
            return Result<TransactionDto>.SafeExecute<TransactionService>(result =>
            {
                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    CategoryEntity attachedCategoryEntity;
                    AccountEntity attachedAccountEntity;
                    
                    //MAPPING
                    var entity = transaction.ToTransactionEntity();

                    if (transaction.CategoryId != 0)
                    {
                        attachedCategoryEntity = datacontext.Category.Where(c => c.id == transaction.CategoryId).First();
                        entity.Category = attachedCategoryEntity;
                    }

                    if (transaction.CategoryId != 0)
                    {
                        attachedAccountEntity = datacontext.Account.Where(c => c.id == transaction.AccountId).First();
                        entity.Account = attachedAccountEntity;
                    }

                


                //Convertion du DTO en entity, puis :
                datacontext.Transaction.InsertOnSubmit(entity);
                datacontext.SubmitChanges();

                var AddedTransac = datacontext.Transaction.Where(a => a.CreatedDate == entity.CreatedDate).First();

                result.Value = AddedTransac.ToTransactionDto();
            
                }
            }, () => "erreur");


        }

        public Result<TransactionDto> UpdateTransaction(TransactionDto transactionToUpdate)
        {
            return Result<TransactionDto>.SafeExecute<TransactionService>(result =>
            {
                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {

                    DataLoadOptions options = new DataLoadOptions();
                    options.LoadWith<Transaction>(c => c.Category);
                    options.LoadWith<Transaction>(c => c.Account);

                    datacontext.LoadOptions = options;


                    var entityToUpdate = datacontext.Transaction.Where(t => t.transactionid == transactionToUpdate.Id).First();

                  
                    entityToUpdate.Name = transactionToUpdate.Name;

                    if (entityToUpdate.Category != null && transactionToUpdate.CategoryId != 0)
                    {
                        if (entityToUpdate.Category.id != transactionToUpdate.CategoryId)
                        {
                            var categoryAttachedByUpdate = datacontext.Category.Where(c => c.id == transactionToUpdate.CategoryId).First();
                            entityToUpdate.Category = categoryAttachedByUpdate;
                        }
                    }

                    if (entityToUpdate.Account != null && transactionToUpdate.AccountId != 0)
                    {
                        if (entityToUpdate.Account.id != transactionToUpdate.AccountId)
                        {
                            var accountAttachedByUpdate = datacontext.Account.Where(c => c.id == transactionToUpdate.AccountId).First();
                            entityToUpdate.Account = accountAttachedByUpdate;
                        }
                    }

                    entityToUpdate.Description = transactionToUpdate.Description;

                    entityToUpdate.Balance = transactionToUpdate.Balance;

                    entityToUpdate.CreatedDate = DateTime.Now;

                    datacontext.SubmitChanges();

                    result.Value = entityToUpdate.ToTransactionDto();

                }
            }, () => "erreur");


        }
    }
}
