using GalaSoft.MvvmLight;
using RMM.Phone.ViewData.Account;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using RMM.Business.CategoryService;
using RMM.Phone.ExtensionMethods;

namespace RMM.Phone.ViewModel
{

    public class EditCategoryViewModel : ViewModelBase
    {
        public CategoryViewData Category { get; set; }

        public RelayCommand DeleteAllTransactionCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public ICategoryService CategoryService { get; set; }

        public EditCategoryViewModel(ICategoryService categoryService)
        {
            CategoryService = categoryService;

            DeleteAllTransactionCommand = new RelayCommand(() => HandleDeleteAllTransactionTaskSelected());
            UpdateCommand = new RelayCommand(() => HandleUpdateTaskSelected());
            CancelCommand = new RelayCommand(() => HandleCancelTaskSelected());


        }

        public void SelectIndex(string categoryId)
        {
            var IntToGo = int.Parse(categoryId);

            var selectedCategory = CategoryService.GetCategoryById(IntToGo, false);

            if (selectedCategory.IsValid)
                Category = selectedCategory.Value.ToCategoryViewData();

            RaisePropertyChanged("Category");
        }

        void HandleDeleteAllTransactionTaskSelected()
        {
            MessageBoxResult result = MessageBox.Show("Do you really want to delete this category ?", "delete " + Category.Name, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                //DELETEALL
            }
        }

        void HandleUpdateTaskSelected()
        {
<<<<<<< HEAD
            var commande = new EditCategoryCommand() { Name = Category.Name, Color = Category.Color, id = Category.Id };
            CategoryService.UpdateCategory(commande);
=======
            var result = CategoryService.UpdateCategory(Category.ToCategoryDto());
            if (result.IsValid)
            {
                var rootFrame = (App.Current as App).RootFrame;
                rootFrame.Navigate(new System.Uri("/MainPage.xaml?update=category", System.UriKind.Relative));
            }
>>>>>>> 7fe9a3ad79d97138946133c3cf951a03a31da1c8
        }

        void HandleCancelTaskSelected()
        {
            var rootFrame = (App.Current as App).RootFrame;
            rootFrame.Navigate(new System.Uri("/MainPage.xaml", System.UriKind.Relative));
        }
    }
}