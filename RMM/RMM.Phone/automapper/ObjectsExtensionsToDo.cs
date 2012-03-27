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
using RMM.Phone.ViewData.Account;
using RMM.Business.TransactionService;
using RMM.Business.AccountService;
using RMM.Business.OptionService;
using RMM.Business.CategoryService;
using RMM.Data.Model;
using Microsoft.Phone.UserData;

namespace RMM.Phone.ExtensionMethods
{
    public static class ObjectsExtensionsToDo
    {

        public static Transaction ToTransaction(this TransactionViewData Objectsource)
        {
            var newtransaction = new Transaction();

            newtransaction.ID = Objectsource.Id;
            newtransaction.Name = Objectsource.Name;
            newtransaction.Description = Objectsource.Description;
            newtransaction.Amount = Objectsource.Amount;

            if (Objectsource.Category != null)
                newtransaction.Category = Objectsource.Category.ToCategoryEntity();

            if (Objectsource.Account != null)
                newtransaction.Account = Objectsource.Account.ToAccountEntity();


            return newtransaction;
        }

        public static AccountEntity ToAccountEntity(this AccountViewData Objectsource)
        {
            var newAccount = new AccountEntity();
            newAccount.ID = Objectsource.Id;
            newAccount.Name = Objectsource.Name;
            newAccount.BankName = Objectsource.BankName;
            newAccount.Balance = Objectsource.Balance;
            newAccount.PhotoUrl = Objectsource.PhotoUrl;

            return newAccount;
        }

        public static Option ToOption(this OptionViewData Objectsource)
        {
            var newOption = new Option();

            newOption.IsComparator = Objectsource.IsComparator;
            newOption.IsPassword = Objectsource.IsPassword;
            newOption.IsPrimaryTile = Objectsource.IsPrimaryTile;
            newOption.IsReport = Objectsource.IsReport;
            newOption.IsSynchro = Objectsource.IsSynchro;
            newOption.ModifiedDate = newOption.ModifiedDate;

            return newOption;

        }

        public static CategoryEntity ToCategoryEntity(this CategoryViewData Objectsource)
        {
            var newCategory = new CategoryEntity();

            newCategory.ID = Objectsource.Id;
            newCategory.Name = Objectsource.Name;
            newCategory.Color = Objectsource.Color;
            newCategory.Balance = Objectsource.Balance;

            return newCategory;
        }

    }
}