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
using RMM.Data;
using RMM.Business.CategoryService;
using RMM.Business.AccountService;
using RMM.Business.TransactionService;
using System.Collections.Generic;
using RMM.Business.OptionService;

namespace RMM.Business
{
    public class DumpMyDBSQLCE
    {

        public static void ProcessDatasOnDB(IAccountService AccountService, ICategoryService CategoryService, ITransactionService TransactionService, IOptionService OptionService)
        {
            #region Categories

            var c1 = new CategoryDto();
            c1.Balance = 7.0;
            c1.Color = "FFA640";
            c1.Name = "Vacances";

            var c2 = new CategoryDto();
            c2.Balance = 7.0;
            c2.Color = "FFA640";
            c2.Name = "Profesionnel";

            #endregion

            #region Accounts

            var na1 = new AccountDto();
            na1.Balance = 7.0;
            na1.BankName = "Credit Agricole";
            na1.Name = "Mon compte courant";

            var na2 = new AccountDto();
            na2.Balance = 7.0;
            na2.BankName = "HSBC";
            na2.Name = "Mon compte courant";

            var na3 = new AccountDto();
            na3.Balance = 7.0;
            na3.BankName = "HSBC";
            na3.Name = "Mon compte epargne HSBC";

       

            #endregion

            #region DB Cleaner

            var Categories = CategoryService.GetAllCategories();

            var Accounts = AccountService.GetAllAccounts();

            if (Categories.Value.Count > 0)
            {
                foreach (var dto in Categories.Value)
                {
                    CategoryService.DeleteCategorieById(dto.Id);
                }
            }

            if (Accounts.Value.Count > 0)
            {
                foreach (var dto in Accounts.Value)
                {
                    AccountService.DeleteAccountById(dto.Id);

                }
            }

            #endregion

            #region Ajout Données

            na1 = AccountService.CreateAccount(na1).Value;
            na2 = AccountService.CreateAccount(na2).Value;
            na3 = AccountService.CreateAccount(na3).Value;

            c1 = CategoryService.CreateCategory(c1).Value;
            c2 = CategoryService.CreateCategory(c2).Value;

            #endregion



            #region Ajout 26 transactions

            var t1 = new TransactionDto() { Name = "redevance tv", AccountId = na1.Id, Balance = -100, Description="test de la description" };
            var t2 = new TransactionDto() { Name = "les courses", AccountId = na1.Id, Balance = -80, Description = "test de la description" };
            var t3 = new TransactionDto() { Name = "restau", AccountId = na2.Id, CategoryId = c2.Id, Balance = -40, Description = "test de la description" };
            var t4 = new TransactionDto() { Name = "piscine", AccountId = na1.Id, Balance = -10, Description = "test de la description" };
            var t5 = new TransactionDto() { Name = "ciné", AccountId = na1.Id, Balance = -5, Description = "test de la description" };
            var t6 = new TransactionDto() { Name = "bar", AccountId = na1.Id, CategoryId = c1.Id, Balance = -10.5, Description = "test de la description" };
            var t7 = new TransactionDto() { Name = "club", AccountId = na1.Id, CategoryId = c1.Id, Balance = -15.5, Description = "test de la description" };
            var t8 = new TransactionDto() { Name = "lotto", AccountId = na1.Id, Balance = 7, Description = "test de la description" };
            var t9 = new TransactionDto() { Name = "la biere", AccountId = na1.Id, Balance = -10, Description = "test de la description" };
            var t10 = new TransactionDto() { Name = "bouquet de fleur", AccountId = na1.Id, Balance = -20, Description = "test de la description" };
            var t11 = new TransactionDto() { Name = "medicaments", AccountId = na1.Id, Balance = -5, Description = "test de la description" };
            var t12 = new TransactionDto() { Name = "pizza tv", AccountId = na1.Id, Balance = -10, Description = "test de la description" };
            var t13 = new TransactionDto() { Name = "moulin de la forge tv", AccountId = na1.Id, CategoryId = c1.Id, Balance = -150, Description = "test de la description" };
            var t14 = new TransactionDto() { Name = "licence MS", AccountId = na2.Id, CategoryId = c2.Id, Balance = -35, Description = "test de la description" };
            var t15 = new TransactionDto() { Name = "James", AccountId = na2.Id, CategoryId = c2.Id, Balance = 500, Description = "test de la description" };
            var t16 = new TransactionDto() { Name = "Ian", AccountId = na2.Id, CategoryId = c2.Id, Balance = 1000, Description = "test de la description" };
            var t17 = new TransactionDto() { Name = "Villa", AccountId = na2.Id, CategoryId = c1.Id, Balance = -200, Description = "test de la description" };
            var t18 = new TransactionDto() { Name = "Telephone", AccountId = na2.Id, CategoryId = c2.Id, Balance = -30, Description = "test de la description" };
            var t19 = new TransactionDto() { Name = "park d'actraction", AccountId = na2.Id, Balance = -20, Description = "test de la description" };
            var t20 = new TransactionDto() { Name = "enfants", AccountId = na2.Id, Balance = 46.7, Description = "test de la description" };
            var t21 = new TransactionDto() { Name = "nouvelle maison", AccountId = na3.Id, Balance = -33.6, Description = "test de la description" };
            var t22 = new TransactionDto() { Name = "epargne taux fixe", AccountId = na3.Id, Balance = 10, Description = "test de la description" };
            var t23 = new TransactionDto() { Name = "epargne sur montpellier", AccountId = na3.Id, Balance = 60, Description = "test de la description" };
            var t24 = new TransactionDto() { Name = "epargne de Paris", AccountId = na3.Id, Balance = -20, Description = "test de la description" };
            var t25 = new TransactionDto() { Name = "investissement", AccountId = na3.Id, Balance = 250, Description = "test de la description" };
            var t26 = new TransactionDto() { Name = "import maman", AccountId = na1.Id, Balance = 700, Description = "test de la description" };

            var listDeTransaction = new List<TransactionDto>();
            listDeTransaction.Add(t1);
            listDeTransaction.Add(t2);
            listDeTransaction.Add(t3);
            listDeTransaction.Add(t4);
            listDeTransaction.Add(t5);
            listDeTransaction.Add(t6);
            listDeTransaction.Add(t7);
            listDeTransaction.Add(t8);
            listDeTransaction.Add(t9);
            listDeTransaction.Add(t10);
            listDeTransaction.Add(t11);
            listDeTransaction.Add(t12);
            listDeTransaction.Add(t13);
            listDeTransaction.Add(t14);
            listDeTransaction.Add(t15);
            listDeTransaction.Add(t16);
            listDeTransaction.Add(t17);
            listDeTransaction.Add(t18);
            listDeTransaction.Add(t19);
            listDeTransaction.Add(t20);
            listDeTransaction.Add(t21);
            listDeTransaction.Add(t22);
            listDeTransaction.Add(t23);
            listDeTransaction.Add(t24);
            listDeTransaction.Add(t25);
            listDeTransaction.Add(t26);


            #endregion

            listDeTransaction.ForEach(transacDto => TransactionService.CreateTransaction(transacDto));

            OptionService.SetFirstTimeOption();
        }

    }
}
