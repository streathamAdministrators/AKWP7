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

        //#region BOOLOPTION

        //private bool isPassword = false;
        //public bool IsPassword
        //{
        //    get { return isPassword; }
        //    set
        //    {
        //        isPassword = value;
        //        RaisePropertyChanged("IsPassword");
        //    }
        //}

        //private bool isSynchro = false;
        //public bool IsSynchro
        //{
        //    get { return isSynchro; }
        //    set
        //    {
        //        isSynchro = value;
        //        RaisePropertyChanged("IsSynchro");
        //    }
        //}

        //private bool isTile = false;
        //public bool IsTile
        //{
        //    get { return isTile; }
        //    set
        //    {
        //        isTile = value;
        //        RaisePropertyChanged("IsTile");
        //    }
        //}

        //private bool isReport = false;
        //public bool IsReport
        //{
        //    get { return isReport; }
        //    set
        //    {
        //        isReport = value;
        //        RaisePropertyChanged("IsReport");
        //    }
        //}

        //private bool isComparator = false;
        //public bool IsComparator 
        //{
        //    get { return isComparator; }
        //    set 
        //    { 
        //        isComparator = value;
        //        RaisePropertyChanged("IsComparator");
        //    }
        //}

        //#endregion

        #region COMMAND

        public RelayCommand<SelectionChangedEventArgs> AccountSelectedCommand { get; set; }
        public RelayCommand<SelectionChangedEventArgs> CategorySelectedCommand { get; set; }

        public RelayCommand<AccountViewData> EditAccountCommand { get; set; }
        public RelayCommand<AccountViewData> DeleteAccountCommand { get; set; }
        public RelayCommand<AccountViewData> FavoriteAccountCommand { get; set; }

        #endregion

        public ObservableCollection<AccountViewData> ListeAccount { get; set; }
        public ObservableCollection<CategoryViewData> ListeCategory { get; set; }

        public AccountViewData FavoriteAccount { get; set; }
        public OptionViewData OptionViewDataObj { get; set; }

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


            DatabaseService = databaseService;
            AccountService = accountService;
            TransactionService = transactionService;
            CategoryService = categoryService;
            OptionService = optionService;

            //A VIRER
           var isAlreadyCreated = DatabaseService.Initialize();

           if (isAlreadyCreated)
            DumpMyDBSQLCE.ProcessDatasOnDB(AccountService, CategoryService, TransactionService, OptionService);


            this.ListeAccount = new ObservableCollection<AccountViewData>();
            this.ListeCategory = new ObservableCollection<CategoryViewData>();

            SetListAccount();
            SetListCategory();
            SetOption();
            SetFavori();

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
                    //LANCER LA COMMANDE DELETEACCOUNTBYID
                }
            }
        }

        private void HandleFavoriteAccountTaskSelected(AccountViewData args)
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
    }
}