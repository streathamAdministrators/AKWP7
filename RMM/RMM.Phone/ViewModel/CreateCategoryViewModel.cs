using GalaSoft.MvvmLight;
using RMM.Phone.ViewData.Account;
using GalaSoft.MvvmLight.Command;

namespace RMM.Phone.ViewModel
{
    public class CreateCategoryViewModel : ViewModelBase
    {
        public CategoryViewData Category { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public CreateCategoryViewModel()
        {
            SaveCommand = new RelayCommand(() => HandleCreateTaskSelected());
            CancelCommand = new RelayCommand(() => HandleCancelTaskSelected());
        }

        void HandleCreateTaskSelected()
        {
            //LANCER LA CREATION ICI
        }

        void HandleCancelTaskSelected()
        {
            var rootFrame = (App.Current as App).RootFrame;
            rootFrame.Navigate(new System.Uri("/MainPage.xaml", System.UriKind.Relative));
        }
    }
}