using GalaSoft.MvvmLight;
using RMM.Phone.ViewData.Account;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using RMM.Business.AccountService;
using RMM.Business.TransactionService;
using RMM.Business.CategoryService;
using RMM.Phone.ExtensionMethods;
using System.Linq;
using System.Windows;
using RMM.Phone.Execution;

namespace RMM.Phone.ViewModel
{

    public class AccountViewModel : BugnionReverseViewModelBase
    {
        public ObservableCollection<AccountViewData> ListeAccount { get; set; }

        private string selectedIndex;
        public string SelectedIndex 
        {
            get { return selectedIndex; }
            set 
            { 
                selectedIndex = value;
                RaisePropertyChanged("SelectedIndex");
            }
        }

        public IAccountService AccountService { get; set; }
        public ICategoryService CategoryService { get; set; }
        public ITransactionService TransactionService { get; set; }

        public AccountViewModel(IAccountService accountService, ITransactionService transactionService, ICategoryService categoryService)
        {
            AccountService = accountService;
            TransactionService = transactionService;
            CategoryService = categoryService;

            ListeAccount = new ObservableCollection<AccountViewData>();

            
            ExecuteSafeDispatcher(() => SetAccounts());
        }

        public void SelectIndex(string accountId)
        {
            int idToGo = int.Parse(accountId);

            var selectedAccount = ListeAccount.Where(avd => avd.Id ==  idToGo).First();

            SelectedIndex = ListeAccount.IndexOf(selectedAccount).ToString();
        }

        private void SetAccounts()
        {
            var resultAccounts = AccountService.GetAllAccounts(false);

            var listAccountsViewData = new List<AccountViewData>();

            if (resultAccounts.IsValid)
            {
                resultAccounts.Value.ForEach(vd => listAccountsViewData.Add(vd.ToAccountViewData()));
            }

            listAccountsViewData
                .ForEach(accountViewData =>
                    {
                        accountViewData.Balance = accountViewData.ListTransaction.Sum(tvd => tvd.Amount);
                        ListeAccount.Add(accountViewData);
                    });
        }   
    }
}