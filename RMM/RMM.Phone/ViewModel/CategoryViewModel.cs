using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using RMM.Phone.ViewData.Account;
using System.Collections.Generic;

namespace RMM.Phone.ViewModel
{
    public class CategoryViewModel : ViewModelBase
    {
        public ObservableCollection<CategoryViewData> ListeCategory { get; set; }
        public List<TransactionViewData> ListeTransaction { get; set; }


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

        public CategoryViewModel()
        {
            ListeCategory = new ObservableCollection<CategoryViewData>();
            ListeTransaction = new List<TransactionViewData>();
            LoadSampleData();
        }

        public void SelectIndex(string accountId)
        {
            //Account account = accountService.getAccountbyId(accountid);
            //selectedIndex = ListeAccount.IndexOf(account).ToString();
            SelectedIndex = "2";

        }

        void LoadSampleData()
        {
            var sampleCategory = new CategoryViewData() { Id = 1, Name = "Holiday", Balance = 856 };
            var sampleCategory2 = new CategoryViewData() { Id = 2, Name = "Food", Balance = 29 };
            var sampleAccount = new AccountViewData() { Id = 1, BankName = "HSBC", Balance = 200, Name = "Courant1" };

            this.ListeTransaction.Add(new TransactionViewData() { ID = 1, Name = "restau ipiuezfhyeif ipuzr zpeoiufpif u", Category = sampleCategory, Account = sampleAccount, Balance = 58987878787.654654 });
            this.ListeTransaction.Add(new TransactionViewData() { ID = 2, Name = "piscine", Category = sampleCategory2, Account = sampleAccount, Balance = 361 });
            this.ListeTransaction.Add(new TransactionViewData() { ID = 3, Name = "ciné", Category = sampleCategory, Account = sampleAccount, Balance = 125 });
            this.ListeTransaction.Add(new TransactionViewData() { ID = 4, Name = "bar", Category = sampleCategory2, Account = sampleAccount, Balance = -410 });
            this.ListeTransaction.Add(new TransactionViewData() { ID = 5, Name = "club", Category = sampleCategory, Account = sampleAccount, Balance = -98 });

            this.ListeCategory.Add(new CategoryViewData() { Id = 1, Name = "Holiday", Balance = 856, ListTransaction = this.ListeTransaction });
            this.ListeCategory.Add(new CategoryViewData() { Id = 2, Name = "Food", Balance = 29, ListTransaction = this.ListeTransaction });
            this.ListeCategory.Add(new CategoryViewData() { Id = 3, Name = "Home", Balance = 352, ListTransaction = this.ListeTransaction });
        }
    }
}