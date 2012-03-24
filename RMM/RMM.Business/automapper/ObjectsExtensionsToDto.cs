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
using RMM.Data.Model;
using RMM.Business.AccountService;
using RMM.Business.OptionService;
using RMM.Business.CategoryService;

namespace RMM.Business.ExtensionMethods
{
    public static class ObjectsExtensionsToDto
    {

        public static  TransactionDto ToTransactionDto(this Transaction Objectsource)
        {
                    var newtransactionDto = new TransactionDto();

                    newtransactionDto.Id = Objectsource.transactionid;
                    newtransactionDto.Name = Objectsource.Name;
                    newtransactionDto.Balance = Objectsource.Balance;

                    if (Objectsource.Category != null)
                    newtransactionDto.CategoryId = Objectsource.Category.id;

                    if (Objectsource.Account != null)
                    newtransactionDto.AccountId = Objectsource.Account.id;

                    newtransactionDto.CreatedDate = Objectsource.CreatedDate;

                    return newtransactionDto;
        }

        public static AccountDto ToAccountDto(this AccountEntity Objectsource)
        {
                var newAccountDto = new AccountDto();

                newAccountDto.Id = Objectsource.id;
                newAccountDto.Name = Objectsource.Name;
                newAccountDto.BankName = Objectsource.BankName;
                newAccountDto.Balance = Objectsource.Balance;
                newAccountDto.PhotoUrl = Objectsource.PhotoUrl;
                newAccountDto.CreatedDate = Objectsource.CreatedDate;

                return newAccountDto;
        }

        public static OptionDto ToOptionDto(this Option Objectsource)
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

        public static CategoryDto ToCategoryDto(this CategoryEntity Objectsource)
        {
                var newCategoryDto = new CategoryDto();

                newCategoryDto.Id = Objectsource.id;
                newCategoryDto.Name = Objectsource.Name;
                newCategoryDto.Color = Objectsource.Color;
                newCategoryDto.Balance = Objectsource.Balance;
                newCategoryDto.CreatedDate = Objectsource.CreatedDate;

               return newCategoryDto;
        }

    }
}
