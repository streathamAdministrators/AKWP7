using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using RMM.Data.Model;

namespace RMM.Data.Service
{
    class TransactionService
    {
        public void DeleteTransactionById(int transactionId)
        {
             using (RmmDataContext datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
             {
                 var transaction = (from t in datacontext.Transaction
                                    where t.ID == transactionId
                                    select t).FirstOrDefault();

                 if (transaction != null)
                 {
                     datacontext.Transaction.DeleteOnSubmit(transaction);
                     datacontext.SubmitChanges();
                 }
             }
        }

        //Retournera un Dto pas un void
        public void GetTransactionById(int transactionId)
        {
            using (RmmDataContext datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
            {
                var transaction = (from t in datacontext.Transaction
                                       where t.Account.ID == transactionId
                                       select t).FirstOrDefault();

                if (transaction != null)
                {
                    //conversion dto
                    //return dto
                }
            }
        }

        //Retournera une liste de Dto pas un void
        public void GetTransactionsByCategoryId(int categoryId)
        {
            using (RmmDataContext datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
            {
                var transactionList = (from t in datacontext.Transaction
                                       where t.Category.ID == categoryId
                                       select t).ToList();

                if (transactionList != null)
                {
                    //conversion en list de dto
                    //return list de dto
                }
            }
        }

        //Retournera une liste de Dto pas un void
        public void GetTransactionsByAccountId(int accountId)
        {
            using (RmmDataContext datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
            {
                var transactionList = (from t in datacontext.Transaction
                                   where t.Account.ID == accountId
                                   select t).ToList();

                if (transactionList != null)
                {
                    //conversion en list de dto
                    //return list de dto
                }
            }
        }

        //Passage d'un dto en param apres
        public void CreateTransaction(Transaction transaction)
        {
            using (RmmDataContext datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
            {
                //Convertion du DTO en entity, puis :
                datacontext.Transaction.InsertOnSubmit(transaction);
                datacontext.SubmitChanges();
            }
        }

        //Passage d'un dto en param apres
        public void UpdateTransaction(Transaction transactionToUpdate)
        {
            using (RmmDataContext datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
            {
                //Convertion du DTO en entity, puis :
                var transaction = (from t in datacontext.Transaction
                                   where t.ID == transactionToUpdate.ID
                                   select t).FirstOrDefault();

                if (transaction != null)
                {
                    //map le dto sur "transaction"
                    datacontext.Transaction.Attach(transaction);
                    //Dire  au datacontext que l'etat de transaction a changé
                    datacontext.SubmitChanges();
                }

                datacontext.SubmitChanges();
            }
        }
    }
}
