using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMM.Business.AccountService
{
    public interface IAccountService
    {
        //Renvoi l'element supprimé
        Result<AccountDto> DeleteAccountById(int accountId);

        //Retournera un Dto
        Result<AccountDto> GetAccountById(int accountId);

        Result<List<AccountDto>> GetAllAccounts();

        //Passage d'un dto en param apres
        Result<AccountDto> CreateAccount(AccountDto account);

        //Passage d'un dto en param apres
        Result<AccountDto> UpdateAccount(AccountDto accountToUpdate);
    }
}
