using GalaSoft.MvvmLight;
using RMM.Phone.ViewData.Account;
using GalaSoft.MvvmLight.Command;
using RMM.Phone.ExtensionMethods;
using RMM.Business.AccountService;

namespace RMM.Phone.ViewModel
{

    public class CreateAccountViewModel : ViewModelBase
    {
        public AccountViewData Account { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public IAccountService AccountService { get; set; }

        public CreateAccountViewModel(IAccountService accountService)
        {
            SaveCommand = new RelayCommand(() => HandleCreateTaskSelected());
            CancelCommand = new RelayCommand(() => HandleCancelTaskSelected());

            AccountService = accountService;

            Account = new AccountViewData();
        }

        void HandleCreateTaskSelected()
        {
            
            AccountService.CreateAccount(new CreateAccountCommand());
        }

        void HandleCancelTaskSelected()
        {
            var rootFrame = (App.Current as App).RootFrame;
            rootFrame.Navigate(new System.Uri("/MainPage.xaml", System.UriKind.Relative));
        }

    }
}