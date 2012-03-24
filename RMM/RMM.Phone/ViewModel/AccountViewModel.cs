using GalaSoft.MvvmLight;
using RMM.Phone.ViewData.Account;
using System.Collections.ObjectModel;

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

        public AccountViewModel()
        {
            ListeAccount = new ObservableCollection<AccountViewData>();
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
            this.ListeAccount.Add(new AccountViewData() { Id = 1, BankName = "HSBC", Balance = 200, Name = "Courant1" });
            this.ListeAccount.Add(new AccountViewData() { Id = 2, BankName = "CA", Balance = 300, Name = "Courant2" });
            this.ListeAccount.Add(new AccountViewData() { Id = 3, BankName = "CA", Balance = 400, Name = "Livret A" });
        }


        
    }
}