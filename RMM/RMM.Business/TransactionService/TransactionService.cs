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
                                           where t.ID == transactionDtoId
                                           select t).FirstOrDefault();
                       
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
                                           where t.Account.ID == transactionId
                                           select t).FirstOrDefault();

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
                    var transactions = (from t in datacontext.Transaction
                                       where t.Category.ID == categoryId
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
                    var transactions = (from t in datacontext.Transaction
                                       where t.Account.ID == accountId
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

                    //MAPPING


                    var entity = transaction.ToTransactionEntity();

                //Convertion du DTO en entity, puis :
                datacontext.Transaction.InsertOnSubmit(entity);
                datacontext.SubmitChanges();

                result.Value = datacontext.Transaction.Where(t => t.ID == transaction.Id).First().ToTransactionDto();
            
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



                    //MAPPING
                    var UpdatedEntity = transactionToUpdate.ToTransactionEntity();

                    

                    var entityToUpdate = datacontext.Transaction.Where(t => t.ID == UpdatedEntity.ID).FirstOrDefault();


                    var categoryAttachedByUpdate = datacontext.Category.Where(c => c.ID == transactionToUpdate.CategoryId).FirstOrDefault();

                    var accountAttachedByUpdate = datacontext.Account.Where(a => a.ID == transactionToUpdate.AccountId).FirstOrDefault();

                    entityToUpdate.Name = UpdatedEntity.Name;

                    if (categoryAttachedByUpdate.ID != entityToUpdate.Category.ID)
                        entityToUpdate.Category = categoryAttachedByUpdate;

                    if (accountAttachedByUpdate.ID != entityToUpdate.Account.ID)
                        entityToUpdate.Account = accountAttachedByUpdate;

                    entityToUpdate.Balance = UpdatedEntity.Balance;

                    datacontext.SubmitChanges();

                }
            }, () => "erreur");


        }
    }
}
