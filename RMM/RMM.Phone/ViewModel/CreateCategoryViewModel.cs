using GalaSoft.MvvmLight;
using RMM.Phone.ViewData.Account;
using GalaSoft.MvvmLight.Command;
using RMM.Business.AccountService;
using RMM.Business.CategoryService;
using RMM.Business.TransactionService;
using RMM.Phone.ExtensionMethods;
using RMM.Phone.Execution;

namespace RMM.Phone.ViewModel
{
    public class CreateCategoryViewModel : BugnionReverseViewModelBase
    {
        public CategoryViewData Category { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        
        public IAccountService AccountService { get; set; }
        public ICategoryService CategoryService { get; set; }
        public ITransactionService TransactionService { get; set; }

        public CreateCategoryViewModel(ICategoryService categoryService)
        {
            SaveCommand = new RelayCommand(() => HandleCreateTaskSelected());
            CancelCommand = new RelayCommand(() => HandleCancelTaskSelected());

            Category = new CategoryViewData();

            CategoryService = categoryService;
        }

        void HandleCreateTaskSelected()
        {
            var newCategoryCommand = new CreateCategoryCommand() { Name = Category.Name, Color = Category.Color };
            CategoryService.CreateCategory(newCategoryCommand);
            NavigateTo("/MainPage.xaml?update=category", null);
        }

        void HandleCancelTaskSelected()
        {
            NavigateTo("/MainPage.xaml", null);
        }
    }
}