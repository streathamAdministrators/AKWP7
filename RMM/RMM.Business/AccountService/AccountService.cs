﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RMM.Data;
using RMM.Business.Helpers;
using RMM.Data.Model;
using System.Linq.Expressions;

namespace RMM.Business.AccountService
{
    public class AccountService: IAccountService
    {
        private RmmDataContext datacontext = null;

        public Result<Account> DeleteAccountById(int accountId)
        {
            return Result<Account>.SafeExecute<AccountService>(result =>
            {
                 
                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    var account = (from t in datacontext.Account
                                    where t.ID == accountId
                                    select t).First();

                    if (account != null)
                    {
                        datacontext.Account.DeleteOnSubmit(account);
                        datacontext.SubmitChanges();
                    }

                    result.Value = account;
                }

            }, () => "error");
        }

        public Result<Account> GetAccountById(int accountId, bool OnMinimal)
        {
            
            return Result<Account>.SafeExecute<AccountService>(result =>
            {
                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    if (!OnMinimal)
                        datacontext.LoadOptions = DBHelpers.GetConfigurationLoader<Account>(acc => acc.TransactionList);

                    var account = datacontext.Account.Where(a => a.ID == accountId).First();


                    result.Value = account;
                }

            }, () => "error");
        }

        public Result<Account> CreateAccount(CreateAccountCommand newAccountCommand)
        {
            return Result<Account>.SafeExecute<AccountService>(result =>
            {
                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    var newAccountEntity = new Account();
                    newAccountEntity.Name = newAccountCommand.Name;
                    newAccountEntity.BankName = newAccountCommand.BankName;
                    newAccountEntity.CreatedDate = DateTime.Now;
                    newAccountEntity.Balance = 0;


                    datacontext.Account.InsertOnSubmit(newAccountEntity);

                    datacontext.SubmitChanges();

                    var AddedAccount = datacontext.Account.Where(a => a.CreatedDate == newAccountEntity.CreatedDate).First();

                    result.Value = AddedAccount;

                }

            }, () => "error");
        }

        public Result<Account> UpdateAccount(EditAccountCommand editAccountCommand)
        {
            return Result<Account>.SafeExecute<AccountService>(result =>
            {

                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {

                    var entityToUpdate = datacontext.Account.Where(t => t.ID == editAccountCommand.id).First();

                    entityToUpdate.ID = editAccountCommand.id;
                    entityToUpdate.Name = editAccountCommand.Name;
                    entityToUpdate.BankName = editAccountCommand.BankName;
                    entityToUpdate.CreatedDate = DateTime.Now;


                    datacontext.SubmitChanges();

                    result.Value = entityToUpdate;
                }

            }, () => "error");
        }


        public Result<List<Account>> GetAllAccounts(bool OnMinimal)
        {
            return Result<List<Account>>.SafeExecute<AccountService>(result =>
            {
                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    if(!OnMinimal)
                     datacontext.LoadOptions =   DBHelpers.GetConfigurationLoader<Account>(Acc => Acc.TransactionList);

                    var accounts = datacontext.Account.ToList();

                    result.Value = accounts;
                }

            }, () => "error");
        }


    }
}
