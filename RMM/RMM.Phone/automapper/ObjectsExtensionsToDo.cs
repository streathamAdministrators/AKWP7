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

namespace RMM.Phone.automapper
{
    public static class ObjectsExtensionsToDo
    {

        public static TransactionDto ToTransactionDto(this TransactionViewData Objectsource)
        {
            var newtransactionDto = new TransactionDto();

            newtransactionDto.Id = Objectsource.Id;
            newtransactionDto.Name = Objectsource.Name;
            newtransactionDto.Description = Objectsource.Description;
            newtransactionDto.Balance = Objectsource.Balance;

            if (Objectsource.Category != null)
                newtransactionDto.CategoryId = Objectsource.Category.Id;

            if (Objectsource.Account != null)
                newtransactionDto.AccountId = Objectsource.Account.Id;


            return newtransactionDto;
        }

        public static AccountDto ToAccountDto(this AccountViewData Objectsource)
        {
            var newAccountDto = new AccountDto();

            newAccountDto.Name = Objectsource.Name;
            newAccountDto.BankName = Objectsource.BankName;
            newAccountDto.Balance = Objectsource.Balance;
            newAccountDto.PhotoUrl = Objectsource.PhotoUrl;

            return newAccountDto;
        }

        public static OptionDto ToOptionDto(this OptionViewData Objectsource)
        {
            var newOptionDto = new OptionDto();

            newOptionDto.IsComparator = Objectsource.IsComparator;
            newOptionDto.IsPassword = Objectsource.IsPassword;
            newOptionDto.IsPrimaryTile = Objectsource.IsPrimaryTile;
            newOptionDto.Isreport = Objectsource.IsReport;
            newOptionDto.IsSynchro = Objectsource.IsSynchro;
            newOptionDto.ModifiedDate = newOptionDto.ModifiedDate;

            return newOptionDto;

        }

        public static CategoryDto ToCategoryDto(this CategoryViewData Objectsource)
        {
            var newCategoryDto = new CategoryDto();

            newCategoryDto.Id = Objectsource.Id;
            newCategoryDto.Name = Objectsource.Name;
            newCategoryDto.Color = Objectsource.Color;
            newCategoryDto.Balance = Objectsource.Balance;

            return newCategoryDto;
        }

    }
}