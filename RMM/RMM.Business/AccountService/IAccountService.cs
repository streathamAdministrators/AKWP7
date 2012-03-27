using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RMM.Data.Model;
using System.Linq.Expressions;

namespace RMM.Business.AccountService
{
    public interface IAccountService
    {

        Result<AccountEntity> DeleteAccountById(int accountId);


        Result<AccountEntity> GetAccountById(int accountId, bool OnMinimal);

        Result<List<AccountEntity>> GetAllAccounts(bool OnMinimal);


        Result<AccountEntity> CreateAccount(CreateAccountCommand newAccountCommand);

        Result<AccountEntity> UpdateAccount(EditAccountCommand editAccountCommand);
    }
}
