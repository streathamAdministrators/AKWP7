using GalaSoft.MvvmLight;
using RMM.Phone.ViewData.Account;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using RMM.Business.AccountService;
using RMM.Phone.ExtensionMethods;

namespace RMM.Phone.ViewModel
{

    public class EditAccountViewModel : ViewModelBase
    {
        public AccountViewData Account { get; set; }

        public RelayCommand DeleteAllTransactionCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public IAccountService Accountservice { get; set; }

        public EditAccountViewModel(IAccountService accountService)
        {
            Accountservice = accountService;

            DeleteAllTransactionCommand = new RelayCommand(() => HandleDeleteAllTransactionTaskSelected());
            UpdateCommand = new RelayCommand(() => HandleUpdateTaskSelected());
            CancelCommand = new RelayCommand(() => HandleCancelTaskSelected());
        }

        public void SelectIndex(string accountId)
        {
            var IntToGo = int.Parse(accountId);

            var selectedAccount = Accountservice.GetAccountById(IntToGo);

            if (selectedAccount.IsValid)
                Account = selectedAccount.Value.ToAccountViewData();

            RaisePropertyChanged("Account");
            
        }

        void HandleDeleteAllTransactionTaskSelected()
        {
            MessageBoxResult result = MessageBox.Show("Do you really want to delete this account ?", "delete " + Account.Name, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                //DELETE ALL
            }
        }

        void HandleUpdateTaskSelected()
        {
            Accountservice.UpdateAccount(Account.ToAccountDto());
        }

        void HandleCancelTaskSelected()
        {
            var rootFrame = (App.Current as App).RootFrame;
            rootFrame.Navigate(new System.Uri("/MainPage.xaml", System.UriKind.Relative));
        }
    }
}