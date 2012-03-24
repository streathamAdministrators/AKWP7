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
                                    where t.id == accountId
                                    select t).First();

                    if (account != null)
                    {
                        datacontext.Account.DeleteOnSubmit(account);
                        datacontext.SubmitChanges();
                    }

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
                    var account = datacontext.Account.Where(a => a.id == accountId).First();


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

                    var AddedAccount = datacontext.Account.Where(a => a.CreatedDate == newEntity.CreatedDate).First();

                    result.Value = AddedAccount.ToAccountDto();

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



                    var entityToUpdate = datacontext.Account.Where(t => t.id == UpdatedEntity.id).First();

                    entityToUpdate.id = UpdatedEntity.id;
                    entityToUpdate.Name = UpdatedEntity.Name;
                    entityToUpdate.BankName = UpdatedEntity.BankName;
                    entityToUpdate.Balance = UpdatedEntity.Balance;
                    entityToUpdate.CreatedDate = DateTime.Now;


                    datacontext.SubmitChanges();

                    result.Value = entityToUpdate.ToAccountDto();
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
