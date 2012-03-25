using GalaSoft.MvvmLight;
using RMM.Phone.ViewData.Account;
using GalaSoft.MvvmLight.Command;

namespace RMM.Phone.ViewModel
{

    public class EditCategoryViewModel : ViewModelBase
    {
        public CategoryViewData Category { get; set; }

        public RelayCommand DeleteAllTransactionCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public EditCategoryViewModel()
        {
            LoadData();

            DeleteAllTransactionCommand = new RelayCommand(() => HandleDeleteAllTransactionTaskSelected());
            UpdateCommand = new RelayCommand(() => HandleUpdateTaskSelected());
            CancelCommand = new RelayCommand(() => HandleCancelTaskSelected());
        }

        public void SelectIndex(string categoryId)
        {
            //GET La category By ID

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
            Category = new CategoryViewData() { Id = 1, Name = "Holiday", Balance = 856 };
        }
    }
}