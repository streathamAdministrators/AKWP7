using GalaSoft.MvvmLight;
using RMM.Phone.ViewData.Account;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace RMM.Phone.ViewModel
{

    public class AccountViewModel : ViewModelBase
    {
        public ObservableCollection<AccountViewData> ListeAccount { get; set; }
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

        public AccountViewModel()
        {
            ListeAccount = new ObservableCollection<AccountViewData>();
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

            this.ListeTransaction.Add(new TransactionViewData() { Id = 1, Name = "restau ipiuezfhyeif ipuzr zpeoiufpif u", Category = sampleCategory, Account = sampleAccount, Balance = 58987878787.654654 });
            this.ListeTransaction.Add(new TransactionViewData() { Id = 2, Name = "piscine", Category = sampleCategory2, Account = sampleAccount, Balance = 361 });
            this.ListeTransaction.Add(new TransactionViewData() { Id = 3, Name = "ciné", Category = sampleCategory, Account = sampleAccount, Balance = 125 });
            this.ListeTransaction.Add(new TransactionViewData() { Id = 4, Name = "bar", Category = sampleCategory2, Account = sampleAccount, Balance = -410 });
            this.ListeTransaction.Add(new TransactionViewData() { Id = 5, Name = "club", Category = sampleCategory, Account = sampleAccount, Balance = -98 });

            this.ListeAccount.Add(new AccountViewData() { Id = 1, BankName = "HSBC", Balance = 200.95, Name = "Courant1", ListTransaction=this.ListeTransaction });
            this.ListeAccount.Add(new AccountViewData() { Id = 2, BankName = "CA", Balance = 300, Name = "Courant2", ListTransaction = this.ListeTransaction });
            this.ListeAccount.Add(new AccountViewData() { Id = 3, BankName = "CA", Balance = 400, Name = "Livret A", ListTransaction = this.ListeTransaction });

        }


        
    }
}