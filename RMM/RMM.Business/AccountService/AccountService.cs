using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RMM.Data;
using RMM.Business.ExtensionMethods;

namespace RMM.Business.AccountService
{
    public class AccountService: IAccountService
    {
        private RmmDataContext datacontext = null;

        public Result<AccountDto> DeleteAccountById(int accountId)
        {
            return Result<AccountDto>.SafeExecute<AccountService>(result =>
            {
                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    var account = (from t in datacontext.Account
                                    where t.ID == accountId
                                    select t).FirstOrDefault();

                    if (account != null)
                        datacontext.Account.DeleteOnSubmit(account);

                    datacontext.SubmitChanges();

                    result.Value = account.ToAccountDto();
                }

            }, () => "error");
        }

        public Result<AccountDto> GetAccountById(int accountId)
        {
            return Result<AccountDto>.SafeExecute<AccountService>(result =>
            {
                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    var account = (from t in datacontext.Account
                                    where t.ID == accountId
                                    select t).FirstOrDefault();


                    result.Value = account.ToAccountDto();
                }

            }, () => "error");
        }

        public Result<AccountDto> CreateAccount(AccountDto account)
        {
            return Result<AccountDto>.SafeExecute<AccountService>(result =>
            {
                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {

                    var newEntity = account.ToAccountEntity();

                    datacontext.Account.InsertOnSubmit(newEntity);

                    datacontext.SubmitChanges();

                    result.Value = account;
                }

            }, () => "error");
        }

        public Result<AccountDto> UpdateAccount(AccountDto accountToUpdate)
        {
            return Result<AccountDto>.SafeExecute<AccountService>(result =>
            {

                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {

                    //MAPPING
                    var UpdatedEntity = accountToUpdate.ToAccountEntity();



                    var entityToUpdate = datacontext.Category.Where(t => t.ID == UpdatedEntity.ID).FirstOrDefault();

                    entityToUpdate.ID = UpdatedEntity.ID;
                    entityToUpdate.Name = UpdatedEntity.Name;
                    entityToUpdate.Color = UpdatedEntity.BankName;
                    entityToUpdate.Balance = UpdatedEntity.Balance;


                    datacontext.SubmitChanges();
                }

            }, () => "error");
        }


        public Result<List<AccountDto>> GetAllAccounts()
        {
            return Result<List<AccountDto>>.SafeExecute<AccountService>(result =>
            {
                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    var accounts = (from t in datacontext.Account
                                   select t).ToList();

                    var listeDto = new List<AccountDto>();

                    accounts.ForEach(account => listeDto.Add(account.ToAccountDto()));

                    result.Value = listeDto;
                }

            }, () => "error");
        }
    }
}
