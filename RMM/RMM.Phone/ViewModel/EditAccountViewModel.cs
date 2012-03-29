using GalaSoft.MvvmLight;
using RMM.Phone.ViewData.Account;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using RMM.Business.AccountService;
using RMM.Phone.ExtensionMethods;
using RMM.Business.CategoryService;
using RMM.Phone.Execution;
using RMM.Business.TransactionService;
using RMM.Business.OptionService;

namespace RMM.Phone.ViewModel
{

    public class EditAccountViewModel : BugnionReverseViewModelBase
    {
        private AccountViewData account;
        public AccountViewData Account
        {
            get { return account; }
            set
            {
                account = value;
                RaisePropertyChanged("Account");
            }
        }

        public bool IsFavorite { get; set; }

        public RelayCommand DeleteAllTransactionCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public IAccountService Accountservice { get; set; }
        public ITransactionService TransactionService { get; set; }
        public IOptionService OptionService { get; set; }

        public EditAccountViewModel(IAccountService accountService, ITransactionService transactionService, IOptionService optionService)
        {
            Accountservice = accountService;
            TransactionService = transactionService;
            OptionService = optionService;

            DeleteAllTransactionCommand = new RelayCommand(() => HandleDeleteAllTransactionTaskSelected());
            UpdateCommand = new RelayCommand(() => HandleUpdateTaskSelected());
            CancelCommand = new RelayCommand(() => HandleCancelTaskSelected());

            Dispose();
        }



        public override void Dispose()
        {
            Account = new AccountViewData();
            base.Dispose();
        }

        public void SelectIndex(string accountId)
        {
            var IntToGo = int.Parse(accountId);

            var selectedAccount = Accountservice.GetAccountById(IntToGo, true);

            if (selectedAccount.IsValid)
                Account = selectedAccount.Value.ToAccountViewData();

            RaisePropertyChanged("Account");
            Account.IsEntityDataChanged = false;


        }

        void HandleDeleteAllTransactionTaskSelected()
        {
            MessageBoxResult result = MessageBox.Show("Do you really want to delete all the transactions ?", "delete " + Account.Name, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                TransactionService.DeleteTransactionsByAccountId(Account.Id);
            }
        }

        void HandleUpdateTaskSelected()
        {
            if (Account.IsEntityDataChanged)
            {
                var editAccountCommand = new EditAccountCommand() { BankName = Account.BankName, id = Account.Id, Name = Account.Name };
                var result = Accountservice.UpdateAccount(editAccountCommand);

                if (IsFavorite)
                    OptionService.SetFavoriteIdAccount(Account.Id);
            }

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