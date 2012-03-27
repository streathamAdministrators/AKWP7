using GalaSoft.MvvmLight;
using RMM.Business.CategoryService;
using System.Collections.ObjectModel;
using RMM.Phone.ViewData.Account;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using System;
using System.Windows.Navigation;
using System.Windows;
using RMM.Business.AccountService;
using RMM.Business.TransactionService;
using RMM.Business.OptionService;
using RMM.Business.DatabaseService;
using RMM.Business;
using System.Linq;
using RMM.Phone.ExtensionMethods;
using System.Collections.Generic;


namespace RMM.Phone.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region HEADER PANORAMA

        private string accountHeader = "Account";
        public string AccountHeader
        {
            get { return accountHeader; }
            set { accountHeader = value; }
        }

        private string favTransactionHeader = "Favorite";
        public string FavTransactionHeader
        {
            get { return favTransactionHeader; }
            set { favTransactionHeader = value; }
        }

        private string categoryHeader = "Category";
        public string CategoryHeader
        {
            get { return categoryHeader; }
            set { categoryHeader = value; }
        }

        private string optionHeader = "Option";
        public string OptionHeader 
        {
            get { return optionHeader; }
            set { optionHeader = value; }
        }

        #endregion

        #region TEXTOPTION

        private string isPasswordTxt  = "Password :";
        public string IsPasswordTxt
        {
            get { return isPasswordTxt; }
            set { isPasswordTxt = value; }
        }

        private string isSynchroTxt = "Synchronisation :";
        public string IsSynchroTxt
        {
            get { return isSynchroTxt; }
            set { isSynchroTxt = value; }
        }

        private string isTileTxt = "Tile :";
        public string IsTileTxt
        {
            get { return isTileTxt; }
            set { isTileTxt = value; }
        }

        private string isReportTxt = "Report :";
        public string IsReportTxt
        {
            get { return isReportTxt; }
            set { isReportTxt = value; }
        }

        private string isComparatorTxt = "Comparator :";
        public string IsComparatorTxt
        {
            get { return isComparatorTxt; }
            set { isComparatorTxt = value; }
        }

        #endregion

        #region COMMAND

        public RelayCommand<SelectionChangedEventArgs> AccountSelectedCommand { get; set; }
        public RelayCommand<SelectionChangedEventArgs> CategorySelectedCommand { get; set; }

        public RelayCommand<AccountViewData> EditAccountCommand { get; set; }
        public RelayCommand<AccountViewData> DeleteAccountCommand { get; set; }
        public RelayCommand<AccountViewData> FavoriteAccountCommand { get; set; }

        public RelayCommand<CategoryViewData> EditCategoryCommand { get; set; }
        public RelayCommand<CategoryViewData> DeleteCategoryCommand { get; set; }
        public RelayCommand<CategoryViewData> FavoriteCategoryCommand { get; set; }

        #endregion

        public ObservableCollection<AccountViewData> ListeAccount { get; set; }
        public ObservableCollection<CategoryViewData> ListeCategory { get; set; }

        public AccountViewData FavoriteAccount { get; set; }
        public OptionViewData OptionViewDataObj { get; set; }

        //public int SelectIndex { get; set; }

        #region Services

        public IDatabaseService DatabaseService { get; set; }
        public IAccountService AccountService { get; set; }
        public ICategoryService CategoryService { get; set; }
        public IOptionService OptionService { get; set; }
        public ITransactionService TransactionService { get; set; }

        #endregion

        public MainViewModel(IAccountService accountService, ICategoryService categoryService, ITransactionService transactionService, IOptionService optionService, IDatabaseService databaseService)
        {
            AccountSelectedCommand = new RelayCommand<SelectionChangedEventArgs>((args) => HandleAccountTaskSelected(args));
            CategorySelectedCommand = new RelayCommand<SelectionChangedEventArgs>((args) => HandleCategoryTaskSelected(args));

            EditAccountCommand = new RelayCommand<AccountViewData>((args) => HandleEditAccountTaskSelected(args));
            DeleteAccountCommand = new RelayCommand<AccountViewData>((args) => HandleDeleteAccountTaskSelected(args));
            FavoriteAccountCommand = new RelayCommand<AccountViewData>((args) => HandleFavoriteAccountTaskSelected(args));

            EditCategoryCommand = new RelayCommand<CategoryViewData>((args) => HandleEditCategoryTaskSelected(args));
            DeleteCategoryCommand = new RelayCommand<CategoryViewData>((args) => HandleDeleteCategoryTaskSelected(args));
            FavoriteCategoryCommand = new RelayCommand<CategoryViewData>((args) => HandleFavoriteCategoryTaskSelected(args));


            DatabaseService = databaseService;
            AccountService = accountService;
            TransactionService = transactionService;
            CategoryService = categoryService;
            OptionService = optionService;

            //A VIRER
            //var isAlreadyCreated = DatabaseService.Initialize();

            //if (isAlreadyCreated)
            //DumpMyDBSQLCE.ProcessDatasOnDB(AccountService, CategoryService, TransactionService, OptionService);


            //this.ListeAccount = new ObservableCollection<AccountViewData>();
            //this.ListeCategory = new ObservableCollection<CategoryViewData>();

            //SetListAccount();
            //SetListCategory();
            //SetOption();
            //SetFavori();

            ProcessDatasOnDB();
            ListeAccount.Add(new AccountViewData() { Balance=253.32, BankName="feouihfje", Name="dfezfazef" });
        }

        public void RefreshAccountAfterUpdate()
        {
            this.ListeAccount = new ObservableCollection<AccountViewData>();
            SetListAccount();
            RaisePropertyChanged("ListeAccount");
        }

        public void RefreshCategoryAfterUpdate()
        {
            this.ListeCategory = new ObservableCollection<CategoryViewData>();
            SetListCategory();
            RaisePropertyChanged("ListeCategory");
            //SelectIndex = 2;
        }

        private void SetFavori()
        {

            FavoriteAccount = this.ListeAccount.First();
            var resultatFavoriteTransaction = TransactionService.GetTransactionsByAccountId(FavoriteAccount.Id);


            var listFavorie = new List<TransactionViewData>();

            if (resultatFavoriteTransaction.IsValid)
                foreach (var dto in resultatFavoriteTransaction.Value)
                {
                    var viewData = dto.ToTransactionViewData();

                    var category = CategoryService.GetCategoryById(dto.CategoryId);

                    if (category.IsValid)
                        viewData.Category = category.Value.ToCategoryViewData();

                    listFavorie.Add(viewData);
                }
            FavoriteAccount.ListTransaction = listFavorie;
        }

        private void SetListAccount()
        {
            var resultatAccountService = AccountService.GetAllAccounts();
            if (resultatAccountService.IsValid)
            {
                resultatAccountService.Value.ForEach(dto => this.ListeAccount.Add(dto.ToAccountViewData()));
            }
        }

        private void SetListCategory()
        {
            var resultatCategoryService = CategoryService.GetAllCategories();
            if (resultatCategoryService.IsValid)
            {
                resultatCategoryService.Value.ForEach(dto => this.ListeCategory.Add(dto.ToCategoryViewData()));
            }
        }

        private void SetOption()
        {
          var resultOption =  OptionService.GetOption();

          if (resultOption.IsValid)
              OptionViewDataObj = resultOption.Value.ToOptionViewData();

        }


        #region Handle on Task Selected

        private void HandleEditAccountTaskSelected(AccountViewData args)
        {
            if (args != null)
            {
                var rootFrame = (App.Current as App).RootFrame;
                rootFrame.Navigate(new System.Uri("/View/EditAccount.xaml?accountId=" + args.Id.ToString(), System.UriKind.Relative));
            }
            
        }

        private void HandleDeleteAccountTaskSelected(AccountViewData args)
        {
            if(args != null)
            {
                MessageBoxResult result = MessageBox.Show("Do you really want to delete " + args.Name + "?"  , "Delete an account", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    var resultat = AccountService.DeleteAccountById(args.Id);
                    RaisePropertyChanged("ListeAccount");
                }
            }
        }

        private void HandleFavoriteAccountTaskSelected(AccountViewData args)
        {
            //AJOUTER L'ACCOUNT AUX FAVORIS
        }


        private void HandleEditCategoryTaskSelected(CategoryViewData args)
        {
            if (args != null)
            {
                var rootFrame = (App.Current as App).RootFrame;
                rootFrame.Navigate(new System.Uri("/View/EditCategory.xaml?categoryId=" + args.Id.ToString(), System.UriKind.Relative));
            }
        }

        private void HandleDeleteCategoryTaskSelected(CategoryViewData args)
        {
            if (args != null)
            {
                MessageBoxResult result = MessageBox.Show("Do you really want to delete " + args.Name + "?", "Delete a category", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    AccountService.DeleteAccountById(args.Id);
                }
            }
        }

        private void HandleFavoriteCategoryTaskSelected(CategoryViewData args)
        {
            //AJOUTER L'ACCOUNT AUX FAVORIS
        }


        private void HandleAccountTaskSelected(SelectionChangedEventArgs args)
        {
            if (args == null) { return; }
            var account = args.AddedItems[0] as AccountViewData;

            var rootFrame = (App.Current as App).RootFrame;
            rootFrame.Navigate(new System.Uri("/View/AccountPivot.xaml?accountId=" + account.Id.ToString(), System.UriKind.Relative));
        }

        private void HandleCategoryTaskSelected(SelectionChangedEventArgs args)
        {
            if (args == null) { return; }
            var category = args.AddedItems[0] as CategoryViewData;

            var rootFrame = (App.Current as App).RootFrame;
            rootFrame.Navigate(new System.Uri("/View/CategoryPivot.xaml?categoryId=" + category.Id.ToString(), System.UriKind.Relative));
        }

        #endregion


         void ProcessDatasOnDB()
        {
            var c1 = new CategoryViewData();
            c1.Balance = 7.0;
            c1.Color = "FFA640";
            c1.Name = "Vacances";
            

            var c2 = new CategoryViewData();
            c2.Balance = 7.0;
            c2.Color = "FFA640";
            c2.Name = "Profesionnel";

            var na1 = new AccountViewData();
            na1.Balance = 7.0;
            na1.BankName = "Credit Agricole";
            na1.Name = "Mon compte courant";

            var na2 = new AccountViewData();
            na2.Balance = 7.0;
            na2.BankName = "HSBC";
            na2.Name = "Mon compte courant";
            na2.Favorite = "Visible";

            var na3 = new AccountViewData();
            na3.Balance = 7.0;
            na3.BankName = "HSBC";
            na3.Name = "Mon compte epargne HSBC";

            this.ListeAccount = new ObservableCollection<AccountViewData>();
            this.ListeAccount.Add(na1);
            this.ListeAccount.Add(na2);
            this.ListeAccount.Add(na3);

            RaisePropertyChanged("ListeAccount");

        }
    }
}