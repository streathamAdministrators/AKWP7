using GalaSoft.MvvmLight;
using RMM.Business.DatabaseService;
using RMM.Business.AccountService;
using RMM.Business.CategoryService;
using RMM.Business.ExtensionMethods;
using RMM.Business.OptionService;
using RMM.Business.TransactionService;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Command;


namespace RMM.Phone.TestFonctionnel.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public enum ActionsEnum {Insert, Get, Update, GetList, Delete, Travail};
        public enum ServiceEnum { Account, Category, Option, Transaction }
        public enum ObjectType { itemId, itemName, itemCount }

        private const string TEMPLATEMessage = "{1} : {0}";
        private const string TEMPLATEMessagedifferService = "On {0}, {1} : {2}";
        private const string TEMPLATEMessageOnUpdate = "{1} : {2} from {1} : {3}";

        public ObservableCollection<KeyValuePair<string, string>> Rendu { get; set; }

        public RelayCommand CategoryRunner { get; set; }
        public RelayCommand AccountRunner { get; set; }
        public RelayCommand TransactionRunner { get; set; }


        public IDatabaseService DatabaseService { get; set; }
        public IAccountService AccountService { get; set; }
        public ICategoryService CategoryService { get; set; }
        public IOptionService OptionService { get; set; }
        public ITransactionService TransactionService { get; set; }

        /// <summary>
        /// The <see cref="WelcomeTitle" /> property's name.
        /// </summary>
        public const string WelcomeTitlePropertyName = "WelcomeTitle";

        private string _welcomeTitle = string.Empty;


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDatabaseService databaseService,
                             IAccountService accountService,
                             ICategoryService categoryService,
                             ITransactionService transactionService,
                             IOptionService optionService)
        {
            DatabaseService = databaseService;
            AccountService = accountService;
            TransactionService = transactionService;
            CategoryService = categoryService;
            OptionService = optionService;




            DatabaseService.Initialize();

            Rendu = new ObservableCollection<KeyValuePair<string, string>>();


            CategoryRunner = new RelayCommand(() =>  TestCategory());
            
            AccountRunner = new RelayCommand(() => TestAccount());

            TransactionRunner = new RelayCommand(() => TestTransaction());
           
        }

        public void addToOc(string action, string value)
        {
            Rendu.Add(new KeyValuePair<string, string>(action, value.ToString()));
            RaisePropertyChanged("Rendu");
        }

        public void TestAccount()
        {
            Rendu.Clear();

            addToOc(ActionsEnum.Travail.ToString(), ServiceEnum.Account.ToString());


            var item = new AccountDto();
            item.Balance = 7.0;
            item.BankName = "AA";
            item.Name = "Mon Compte 2";

            var Accounts = AccountService.GetAllAccounts();

            addToOc(ActionsEnum.GetList.ToString(), string.Format(TEMPLATEMessage,ObjectType.itemCount.ToString(), Accounts.Value.Count.ToString()));


            if (Accounts.Value.Count > 0)
            {
                foreach (var dto in Accounts.Value)
                {
                    AccountService.DeleteAccountById(dto.Id);

                    addToOc(ActionsEnum.Delete.ToString(), string.Format(TEMPLATEMessage,  ObjectType.itemId.ToString(), dto.Id.ToString()));


                }

                Accounts = AccountService.GetAllAccounts();

                addToOc(ActionsEnum.GetList.ToString(), string.Format(TEMPLATEMessage, ObjectType.itemCount.ToString(), Accounts.Value.Count.ToString()));

            }




          var addedaccount =  AccountService.CreateAccount(item);

          addToOc(ActionsEnum.Insert.ToString(),string.Format(TEMPLATEMessage,ObjectType.itemId.ToString(), addedaccount.Value.Id.ToString()));

          var Account = AccountService.GetAccountById(addedaccount.Value.Id);

          addToOc(ActionsEnum.Get.ToString(), string.Format(TEMPLATEMessage, ObjectType.itemId.ToString(), Account.Value.Id));


            Account.Value.BankName = "TESTER";

            var updatedAccount = AccountService.UpdateAccount(Account.Value);

            addToOc(ActionsEnum.Update.ToString(), string.Format(TEMPLATEMessageOnUpdate, ServiceEnum.Account.ToString(), ObjectType.itemName.ToString(), updatedAccount.Value.BankName, addedaccount.Value.BankName));

        }

        public void TestCategory()
        {
            Rendu.Clear();

            addToOc(ActionsEnum.Travail.ToString(), ServiceEnum.Category.ToString());

            var item = new CategoryDto();
            item.Balance = 7.0;
            item.Color = "AA";
            item.Name = "MyCategory";

            var Categories = CategoryService.GetAllCategories();

            addToOc(ActionsEnum.GetList.ToString(),string.Format(TEMPLATEMessage,ObjectType.itemCount.ToString(), Categories.Value.Count));

            if (Categories.Value.Count > 0)
            {
                foreach (var dto in Categories.Value)
                {
                    CategoryService.DeleteCategorieById(dto.Id);
                    addToOc(ActionsEnum.Delete.ToString(),string.Format(TEMPLATEMessage,ObjectType.itemId.ToString(),  dto.Id));
                }
                
            Categories = CategoryService.GetAllCategories();

            addToOc(ActionsEnum.GetList.ToString(),string.Format(TEMPLATEMessage, ObjectType.itemCount.ToString(),  Categories.Value.Count));
            }



            var addedCategory = CategoryService.CreateCategory(item);

            addToOc(ActionsEnum.Insert.ToString(),string.Format(TEMPLATEMessage, ObjectType.itemId.ToString(), addedCategory.Value.Id));


            var category = CategoryService.GetCategoryById(addedCategory.Value.Id).Value;

            addToOc(ActionsEnum.Get.ToString(),string.Format(TEMPLATEMessage, ObjectType.itemId.ToString(), category.Id));


            category.Name = "LALALALALA";

           var updatedCategory = CategoryService.UpdateCategory(category);

           addToOc(ActionsEnum.Update.ToString(), string.Format(TEMPLATEMessageOnUpdate, ServiceEnum.Category.ToString(), ObjectType.itemName.ToString(), updatedCategory.Value.Name, addedCategory.Value.Name));

        }

        public void TestTransaction()
        {
            Rendu.Clear();

            addToOc(ActionsEnum.Travail.ToString(), ServiceEnum.Transaction.ToString());

            #region Nouvelle Categorie, Nouveau Account

            var newCategory = new CategoryDto();
            newCategory.Balance = 7.0;
            newCategory.Color = "AA";
            newCategory.Name = "MyCategory";

            var newAccount = new AccountDto();
            newAccount.Balance = 7.0;
            newAccount.BankName = "AA";
            newAccount.Name = "MyAccount";

            #endregion

            #region Nettoyage Base

            var Categories = CategoryService.GetAllCategories();

            addToOc(ActionsEnum.GetList.ToString(), string.Format(TEMPLATEMessagedifferService, ServiceEnum.Category.ToString(), ObjectType.itemCount.ToString(), Categories.Value.Count));


            var Accounts = AccountService.GetAllAccounts();

            addToOc(ActionsEnum.GetList.ToString(), string.Format(TEMPLATEMessagedifferService, ServiceEnum.Account.ToString(), ObjectType.itemCount.ToString(), Accounts.Value.Count));



            if (Categories.Value.Count > 0)
            {
                foreach (var dto in Categories.Value)
                {
                    CategoryService.DeleteCategorieById(dto.Id);
                    addToOc(ActionsEnum.Delete.ToString(), string.Format(TEMPLATEMessagedifferService, ServiceEnum.Category.ToString(), ObjectType.itemId.ToString(), dto.Id));
                }

                Categories = CategoryService.GetAllCategories();

                addToOc(ActionsEnum.GetList.ToString(), string.Format(TEMPLATEMessagedifferService, ServiceEnum.Category.ToString(), ObjectType.itemCount.ToString(), Categories.Value.Count));
            }

            if (Accounts.Value.Count > 0)
            {
                foreach (var dto in Accounts.Value)
                {
                    AccountService.DeleteAccountById(dto.Id);
                    addToOc(ActionsEnum.Delete.ToString(), string.Format(TEMPLATEMessagedifferService, ServiceEnum.Account.ToString(), ObjectType.itemId.ToString(), dto.Id));
                }

                Accounts = AccountService.GetAllAccounts();

                addToOc(ActionsEnum.GetList.ToString(), string.Format(TEMPLATEMessagedifferService, ServiceEnum.Account.ToString(), ObjectType.itemCount.ToString(), Accounts.Value.Count));
            }

            #endregion

            #region Ajout Category, Account

            var addedCategory = CategoryService.CreateCategory(newCategory);

            addToOc(ActionsEnum.Insert.ToString(), string.Format(TEMPLATEMessagedifferService, ServiceEnum.Category.ToString(), ObjectType.itemId.ToString(), addedCategory.Value.Id));


            var addedAccount = AccountService.CreateAccount(newAccount);

            addToOc(ActionsEnum.Insert.ToString(), string.Format(TEMPLATEMessagedifferService, ServiceEnum.Account.ToString(), ObjectType.itemId.ToString(), addedAccount.Value.Id));

            #endregion


            #region Ajout 2 transactions, same account, 1 with the category

            var newTransaction = new TransactionDto();
            newTransaction.Name = "First Transaction !";
            newTransaction.Balance = 40.0;
            newTransaction.AccountId = addedAccount.Value.Id;
            newTransaction.CategoryId = addedCategory.Value.Id;

            var newTransaction2 = new TransactionDto();
            newTransaction2.Name = "Second Transaction !";
            newTransaction2.Balance = 40.0;
            newTransaction2.AccountId = addedAccount.Value.Id;

            var addedtransaction = TransactionService.CreateTransaction(newTransaction);

            addToOc(ActionsEnum.Insert.ToString(), string.Format(TEMPLATEMessagedifferService, ServiceEnum.Transaction.ToString(), ObjectType.itemName.ToString(), addedtransaction.Value.Name));

            var addedtransaction2 = TransactionService.CreateTransaction(newTransaction);

            addToOc(ActionsEnum.Insert.ToString(), string.Format(TEMPLATEMessagedifferService, ServiceEnum.Transaction.ToString(), ObjectType.itemName.ToString(), addedtransaction2.Value.Name));

            #endregion

            #region get des listes par account et par category

            var listebyAccount = TransactionService.GetTransactionsByAccountId(addedAccount.Value.Id);

            addToOc(ActionsEnum.GetList.ToString(), string.Format(TEMPLATEMessagedifferService, ServiceEnum.Transaction.ToString(), ObjectType.itemCount.ToString(), listebyAccount.Value.Count));


           var listebyCategory = TransactionService.GetTransactionsByCategoryId(addedCategory.Value.Id);

           addToOc(ActionsEnum.GetList.ToString(), string.Format(TEMPLATEMessagedifferService, ServiceEnum.Transaction.ToString(), ObjectType.itemCount.ToString(), listebyCategory.Value.Count));

            #endregion

           var gettedTrans = TransactionService.GetTransactionById(addedtransaction.Value.Id).Value;

           addToOc(ActionsEnum.Get.ToString(), string.Format(TEMPLATEMessagedifferService, ServiceEnum.Transaction.ToString(), ObjectType.itemName.ToString(), gettedTrans.Name));


           gettedTrans.Name = "MODIFIED";

           var updatedCategory = TransactionService.UpdateTransaction(gettedTrans);

           addToOc(ActionsEnum.Update.ToString(), string.Format(TEMPLATEMessageOnUpdate, ServiceEnum.Transaction.ToString(), ObjectType.itemName.ToString(), updatedCategory.Value.Name, addedtransaction.Value.Name));

        }



        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}