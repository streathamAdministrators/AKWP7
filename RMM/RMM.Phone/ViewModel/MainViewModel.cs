using GalaSoft.MvvmLight;
using RMM.Business.CategoryService;
using System.Collections.ObjectModel;
using RMM.Phone.ViewData.Account;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using System;
using System.Windows.Navigation;


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

        #region BOOLOPTION

        private bool isPassword = false;
        public bool IsPassword
        {
            get { return isPassword; }
            set
            {
                isPassword = value;
                RaisePropertyChanged("IsPassword");
            }
        }

        private bool isSynchro = false;
        public bool IsSynchro
        {
            get { return isSynchro; }
            set
            {
                isSynchro = value;
                RaisePropertyChanged("IsSynchro");
            }
        }

        private bool isTile = false;
        public bool IsTile
        {
            get { return isTile; }
            set
            {
                isTile = value;
                RaisePropertyChanged("IsTile");
            }
        }

        private bool isReport = false;
        public bool IsReport
        {
            get { return isReport; }
            set
            {
                isReport = value;
                RaisePropertyChanged("IsReport");
            }
        }

        private bool isComparator = false;
        public bool IsComparator 
        {
            get { return isComparator; }
            set 
            { 
                isComparator = value;
                RaisePropertyChanged("IsComparator");
            }
        }

        #endregion

        #region COMMAND

        public RelayCommand<SelectionChangedEventArgs> AccountSelectedCommand { get; set; }
        public RelayCommand<SelectionChangedEventArgs> CategorySelectedCommand { get; set; }

        public RelayCommand EditAccountCommand { get; set; }

        #endregion

        

        public ObservableCollection<AccountViewData> ListeAccount { get; set; }
        public ObservableCollection<CategoryViewData> ListeCategory { get; set; }
        public ObservableCollection<TransactionViewData> ListeFavorite { get; set; }
        public AccountViewData FavoriteAccount { get; set; }

        public MainViewModel()
        {
            this.ListeAccount = new ObservableCollection<AccountViewData>();
            this.ListeCategory = new ObservableCollection<CategoryViewData>();
            this.ListeFavorite = new ObservableCollection<TransactionViewData>();

            AccountSelectedCommand = new RelayCommand<SelectionChangedEventArgs>((args) => HandleAccountTaskSelected(args));
            CategorySelectedCommand = new RelayCommand<SelectionChangedEventArgs>((args) => HandleCategoryTaskSelected(args));

            EditAccountCommand = new RelayCommand(() => HandleEditAccountTaskSelected() );

            LoadSampleData();
            IsSynchro = true;
        }

        private void HandleEditAccountTaskSelected()
        {
 
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


        void LoadSampleData() 
        {
            var sampleCategory = new CategoryViewData() { Id = 1 , Name = "Holiday" , Balance = 856 };
            var sampleCategory2 = new CategoryViewData() { Id = 2 , Name = "Food" , Balance = 29 } ;
            var sampleAccount = new AccountViewData() { Id = 1, BankName = "HSBC", Balance = 200, Name = "Courant1" };

            this.ListeAccount.Add(sampleAccount);
            this.ListeAccount.Add(new AccountViewData() { Id = 2, BankName = "CA", Balance = 300, Name = "Courant2" });
            this.ListeAccount.Add(new AccountViewData() { Id = 3, BankName = "CA", Balance = 400, Name = "Livret A" });

            this.ListeCategory.Add(sampleCategory);
            this.ListeCategory.Add(sampleCategory2);
            this.ListeCategory.Add(new CategoryViewData() { Id = 3, Name = "Home", Balance = 352 });

            this.ListeFavorite.Add(new TransactionViewData() { Id = 1, Name = "restau ipiuezfhyeif ipuzr zpeoiufpif u", Category = sampleCategory, Account = sampleAccount, Balance = 58987878787.654654 });
            this.ListeFavorite.Add(new TransactionViewData() { Id = 2, Name = "piscine", Category = sampleCategory2, Account = sampleAccount, Balance = 361 });
            this.ListeFavorite.Add(new TransactionViewData() { Id = 3, Name = "ciné", Category = sampleCategory, Account = sampleAccount, Balance = 125 });
            this.ListeFavorite.Add(new TransactionViewData() { Id = 4, Name = "bar", Category = sampleCategory2, Account = sampleAccount, Balance = -410 });
            this.ListeFavorite.Add(new TransactionViewData() { Id = 5, Name = "club", Category = sampleCategory, Account = sampleAccount, Balance = -98 });

            FavoriteAccount = sampleAccount;
        }

    }
}