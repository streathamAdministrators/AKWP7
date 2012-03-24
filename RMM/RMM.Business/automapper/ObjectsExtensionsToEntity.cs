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

                newTransaction.ID = Objectsource.Id;
                newTransaction.Name = Objectsource.Name;

               return newTransaction;

        }

        public static AccountEntity ToAccountEntity(this AccountDto Objectsource)
        {
                var newAccount = new AccountEntity();

                newAccount.ID = Objectsource.Id;
                newAccount.Name = Objectsource.Name;
                newAccount.BankName = Objectsource.BankName;
                newAccount.Balance = Objectsource.Balance;
                newAccount.PhotoUrl = Objectsource.PhotoUrl;

                return newAccount;
        }

        public static Option ToOptionEntity(this OptionDto Objectsource)
        {
                var newOption = new Option();

                newOption.ID = Objectsource.Id;
                newOption.IsComparator = Objectsource.IsComparator;
                newOption.IsPassword = Objectsource.IsPassword;
                newOption.IsPrimaryTile = Objectsource.IsPrimaryTile;
                newOption.IsReport = Objectsource.Isreport;
                newOption.IsSynchro = Objectsource.IsSynchro;

                return newOption;
        }

        public static CategoryEntity ToCategoryEntity(this CategoryDto Objectsource)
        {
                var newCategory = new CategoryEntity();

                newCategory.ID = Objectsource.Id;
                newCategory.Name = Objectsource.Name;
                newCategory.Balance = Objectsource.Balance;
                newCategory.Color = Objectsource.Color;

                return newCategory;
        }
    }
}
