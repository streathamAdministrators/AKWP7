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

        Result<Account> DeleteAccountById(int accountId);


        Result<Account> GetAccountById(int accountId, bool OnMinimal);

        Result<List<Account>> GetAllAccounts(bool OnMinimal);


        Result<Account> CreateAccount(CreateAccountCommand newAccountCommand);

        Result<Account> UpdateAccount(EditAccountCommand editAccountCommand);
    }
}
