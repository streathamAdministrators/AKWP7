using GalaSoft.MvvmLight;
using RMM.Phone.ViewData.Account;
using GalaSoft.MvvmLight.Command;

namespace RMM.Phone.ViewModel
{

    public class EditAccountViewModel : ViewModelBase
    {
        public AccountViewData Account { get; set; }

        public RelayCommand DeleteAllTransactionCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public EditAccountViewModel()
        {
            LoadData();

            DeleteAllTransactionCommand = new RelayCommand(() => HandleDeleteAllTransactionTaskSelected());
            UpdateCommand = new RelayCommand(() => HandleUpdateTaskSelected());
            CancelCommand = new RelayCommand(() => HandleCancelTaskSelected());
        }

        public void SelectIndex(string accountId)
        {
            //GET L'ACCOUNT By ID
            
        }

        void HandleDeleteAllTransactionTaskSelected()
        {
            //LANCER LE DELETE ICI
        }

        void HandleUpdateTaskSelected()
        {
            //LANCER L'UPDATE ICI
        }

        void HandleCancelTaskSelected()
        {
            var rootFrame = (App.Current as App).RootFrame;
            rootFrame.Navigate(new System.Uri("/MainPage.xaml", System.UriKind.Relative));
        }

        void LoadData()
        {
            Account = new AccountViewData() { Id = 1, BankName = "HSBC", Balance = 200.95, Name = "Courant1" };
        }
    }
}