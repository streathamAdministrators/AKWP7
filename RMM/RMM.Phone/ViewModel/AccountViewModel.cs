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

namespace RMM.Phone.ViewModel
{

    public class AccountViewModel : ViewModelBase
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

            Deployment.Current.Dispatcher.BeginInvoke(() => SetAccounts());
            
        }

        public void SelectIndex(string accountId)
        {
            int idToGo = int.Parse(accountId);

            var selectedAccount = ListeAccount.Where(avd => avd.Id ==  idToGo).First();

            SelectedIndex = ListeAccount.IndexOf(selectedAccount).ToString();
        }

        private void SetAccounts()
        {
            var resultAccounts = AccountService.GetAllAccounts();

            var listAccountsViewData = new List<AccountViewData>();

            if (resultAccounts.IsValid)
            {
                resultAccounts.Value.ForEach(dto => listAccountsViewData.Add(dto.ToAccountViewData()));
            }

            listAccountsViewData
                .ForEach(accountViewData =>
                    {
                        accountViewData.ListTransaction = getTransactionForAccountViewData(accountViewData);
                        accountViewData.Balance = accountViewData.ListTransaction.Sum(tvd => tvd.Balance);
                        ListeAccount.Add(accountViewData);
                    });
        }

        private List<TransactionViewData> getTransactionForAccountViewData(AccountViewData accountViewData)
        {
            var resultatFavoriteTransaction = TransactionService.GetTransactionsByAccountId(accountViewData.Id);


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

            return listFavorie;
        }






        
    }
}