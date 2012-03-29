using GalaSoft.MvvmLight;
using RMM.Phone.ViewData.Account;
using GalaSoft.MvvmLight.Command;
using RMM.Phone.ExtensionMethods;
using RMM.Business.AccountService;
using RMM.Phone.Execution;

namespace RMM.Phone.ViewModel
{

    public class CreateAccountViewModel : BugnionReverseViewModelBase
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

            Dispose();
        }


        public override void Dispose()
        {
            Account = new AccountViewData();
            base.Dispose();
        }


        void HandleCreateTaskSelected()
        {

            AccountService.CreateAccount(new CreateAccountCommand() { Name = Account.Name, BankName = Account.BankName });

            NavigateTo("/MainPage.xaml?update=account", null);

            Dispose();
        }

        void HandleCancelTaskSelected()
        {

            NavigateTo("/MainPage.xaml", null);

            Dispose();
        }

    }
}