using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using RMM.Business.TransactionService;
using RMM.Business.AccountService;
using RMM.Business.OptionService;
using RMM.Business.CategoryService;
using RMM.Data.Model;
using System.Collections.Generic;
using System.Data.Linq;

namespace RMM.Business.ExtensionMethods
{
    public static class ObjectsExtensionsToEntity
    {
        public static Transaction ToTransactionEntity(this TransactionDto Objectsource)
        {
                var newTransaction = new Transaction();

                newTransaction.transactionid = Objectsource.Id;
                newTransaction.Name = Objectsource.Name;
                newTransaction.Description = Objectsource.Description;
                newTransaction.Balance = Objectsource.Balance;
                newTransaction.CreatedDate = DateTime.Now;

               return newTransaction;

        }

        public static AccountEntity ToAccountEntity(this AccountDto Objectsource)
        {
                var newAccount = new AccountEntity();

                newAccount.id = Objectsource.Id;
                newAccount.Name = Objectsource.Name;
                newAccount.BankName = Objectsource.BankName;
                newAccount.Balance = Objectsource.Balance;
                newAccount.PhotoUrl = Objectsource.PhotoUrl;
                newAccount.CreatedDate = DateTime.Now;

                return newAccount;
        }

        public static Option ToOptionEntity(this OptionDto Objectsource)
        {
                var newOption = new Option();
                newOption.id = 1;
                newOption.IsComparator = Objectsource.IsComparator;
                newOption.IsPassword = Objectsource.IsPassword;
                newOption.IsPrimaryTile = Objectsource.IsPrimaryTile;
                newOption.IsReport = Objectsource.Isreport;
                newOption.IsSynchro = Objectsource.IsSynchro;
                newOption.ModifiedDate = DateTime.Now;

                return newOption;
        }

        public static CategoryEntity ToCategoryEntity(this CategoryDto Objectsource)
        {
                var newCategory = new CategoryEntity();

                newCategory.id = Objectsource.Id;
                newCategory.Name = Objectsource.Name;
                newCategory.Balance = Objectsource.Balance;
                newCategory.Color = Objectsource.Color;
                newCategory.CreatedDate = DateTime.Now;

                return newCategory;
        }
    }
}
