using GalaSoft.MvvmLight;
using RMM.Phone.ViewData.Account;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using RMM.Business.CategoryService;
using RMM.Phone.ExtensionMethods;
using RMM.Business.TransactionService;
using RMM.Phone.Execution;

namespace RMM.Phone.ViewModel
{

    public class EditCategoryViewModel : BugnionReverseViewModelBase
    {

        private CategoryViewData category;
        public CategoryViewData Category
        {
            get { return category; }
            set
            {
                category = value;
            }
        }



        public RelayCommand DeleteAllTransactionCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public ICategoryService CategoryService { get; set; }
        public ITransactionService TransactionService { get; set; }

        public EditCategoryViewModel(ICategoryService categoryService, ITransactionService transactionService)
        {
            CategoryService = categoryService;
            TransactionService = transactionService;

            DeleteAllTransactionCommand = new RelayCommand(() => HandleDeleteAllTransactionTaskSelected());
            UpdateCommand = new RelayCommand(() => HandleUpdateTaskSelected());
            CancelCommand = new RelayCommand(() => HandleCancelTaskSelected());

            Dispose();
        }

        public override void Dispose()
        {
            Category = new CategoryViewData();
            base.Dispose();
        }


        public void SelectIndex(string categoryId)
        {
            var IntToGo = int.Parse(categoryId);

            var selectedCategory = CategoryService.GetCategoryById(IntToGo, false);

            if (selectedCategory.IsValid)
                Category = selectedCategory.Value.ToCategoryViewData();

            RaisePropertyChanged("Category");
            Category.IsEntityDataChanged = false;
        }

        void HandleDeleteAllTransactionTaskSelected()
        {
            MessageBoxResult result = MessageBox.Show("Do you really want to delete this category ?", "delete " + Category.Name, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                TransactionService.DeleteTransactionsByCategoryId(Category.Id);
            }
        }

        void HandleUpdateTaskSelected()
        {
            if (Category.IsEntityDataChanged)
            {
                var commande = new EditCategoryCommand() { Name = Category.Name, Color = Category.Color, id = Category.Id };
                CategoryService.UpdateCategory(commande);

                var result = CategoryService.UpdateCategory(commande);

            }
            NavigateTo("/MainPage.xaml?update=category", null);

            Dispose();
        }

        void HandleCancelTaskSelected()
        {
            NavigateTo("/MainPage.xaml", null);

            Dispose();
        }



    }
}