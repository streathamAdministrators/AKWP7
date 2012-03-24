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
using RMM.Phone.ViewData.Account;
using RMM.Business.AccountService;
using RMM.Business.OptionService;
using RMM.Business.CategoryService;

namespace RMM.Phone.automapper
{
    public static class ObjectsExtensionsToViewData
    {
        public static TransactionViewData ToTransactionViewData(this TransactionDto Objectsource)
        {
            var newTransactionViewData = new TransactionViewData();

            newTransactionViewData.Id = Objectsource.Id;
            newTransactionViewData.Name = Objectsource.Name;
            newTransactionViewData.Description = Objectsource.Description;
            newTransactionViewData.Balance = Objectsource.Balance;
            newTransactionViewData.CreatedDate = Objectsource.CreatedDate.ToShortDateString();

            return newTransactionViewData;

        }

        public static AccountViewData ToAccountViewData(this AccountDto Objectsource)
        {
            var newAccountViewData = new AccountViewData();

            newAccountViewData.Id = Objectsource.Id;
            newAccountViewData.Name = Objectsource.Name;
            newAccountViewData.BankName = Objectsource.BankName;
            newAccountViewData.Balance = Objectsource.Balance;
            newAccountViewData.PhotoUrl = Objectsource.PhotoUrl;

            return newAccountViewData;
        }

        public static OptionViewData ToOptionViewData(this OptionDto Objectsource)
        {
            var newOptionViewData = new OptionViewData();
            newOptionViewData.IsComparator = Objectsource.IsComparator;
            newOptionViewData.IsPassword = Objectsource.IsPassword;
            newOptionViewData.IsPrimaryTile = Objectsource.IsPrimaryTile;
            newOptionViewData.IsReport = Objectsource.Isreport;
            newOptionViewData.IsSynchro = Objectsource.IsSynchro;

            return newOptionViewData;
        }

        public static CategoryViewData ToCategoryViewData(this CategoryDto Objectsource)
        {
            var newCategoryViewData = new CategoryViewData();

            newCategoryViewData.Id = Objectsource.Id;
            newCategoryViewData.Name = Objectsource.Name;
            newCategoryViewData.Balance = Objectsource.Balance;
            newCategoryViewData.Color = Objectsource.Color;

            return newCategoryViewData;
        }
    }
}
