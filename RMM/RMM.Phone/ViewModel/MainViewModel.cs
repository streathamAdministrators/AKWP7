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

        public RelayCommand<SelectionChangedEventArgs> AccountSelectedCommand { get; set; }

        public ObservableCollection<AccountViewData> ListeAccount { get; set; }

        public MainViewModel()
        {
            this.ListeAccount = new ObservableCollection<AccountViewData>();
            AccountSelectedCommand = new RelayCommand<SelectionChangedEventArgs>((args) => HandleAccountTaskSelected(args));
            LoadSampleData();
        }

        private void HandleAccountTaskSelected(SelectionChangedEventArgs args)
        {
            if (args == null) { return; }
            var account = args.AddedItems[0] as AccountViewData;

            var rootFrame = (App.Current as App).RootFrame;
            rootFrame.Navigate(new System.Uri("/View/AccountPivot.xaml?accountId=" + account.Id.ToString(), System.UriKind.Relative));

        } 


        void LoadSampleData() 
        {
            this.ListeAccount.Add(new AccountViewData() { Id = 1, BankName = "HSBC", Balance = 200, Name = "Courant" });
            this.ListeAccount.Add(new AccountViewData() { Id = 2, BankName = "CA", Balance = 300, Name = "Courant" });
            this.ListeAccount.Add(new AccountViewData() { Id = 3, BankName = "CA", Balance = 400, Name = "Livret A" });
        }

    }
}