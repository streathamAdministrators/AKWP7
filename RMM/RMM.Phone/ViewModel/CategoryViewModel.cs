using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using RMM.Phone.ViewData.Account;
using System.Collections.Generic;
using RMM.Business.AccountService;
using RMM.Business.CategoryService;
using RMM.Business.TransactionService;
using RMM.Phone.ExtensionMethods;
using System.Windows;
using System.Linq;
using RMM.Phone.Execution;

namespace RMM.Phone.ViewModel
{
    public class CategoryViewModel : BugnionReverseViewModelBase
    {
        public ObservableCollection<CategoryViewData> ListeCategory { get; set; }


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

        public CategoryViewModel(IAccountService accountService, ITransactionService transactionService, ICategoryService categoryService)
        {

            AccountService = accountService;
            TransactionService = transactionService;
            CategoryService = categoryService;

            ListeCategory = new ObservableCollection<CategoryViewData>();

            ExecuteSafeDispatcher(() => SetCategories());
        }

        public void SelectIndex(string accountId)
        {
            int idToGo = int.Parse(accountId);

            var selectedCategory = ListeCategory.Where(cvd => cvd.Id == idToGo).First();

            SelectedIndex = ListeCategory.IndexOf(selectedCategory).ToString();
        }

        private void SetCategories()
        {
            var resultCategories = CategoryService.GetAllCategories(false);

            if (resultCategories.IsValid)
            {
                resultCategories.Value.ForEach(dto => this.ListeCategory.Add(dto.ToCategoryViewData()));
            }
        }
    }
}