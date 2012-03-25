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

namespace RMM.Phone.ViewModel
{
    public class CategoryViewModel : ViewModelBase
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

            Deployment.Current.Dispatcher.BeginInvoke(() => SetCategories());

            
        }

        public void SelectIndex(string accountId)
        {
            int idToGo = int.Parse(accountId);

            var selectedCategory = ListeCategory.Where(cvd => cvd.Id == idToGo).First();

            SelectedIndex = ListeCategory.IndexOf(selectedCategory).ToString();
        }

        private void SetCategories()
        {
            var resultCategories = CategoryService.GetAllCategories();

            var listCategoriesViewData = new List<CategoryViewData>();

            if (resultCategories.IsValid)
            {
                resultCategories.Value.ForEach(dto => listCategoriesViewData.Add(dto.ToCategoryViewData()));
            }

            listCategoriesViewData
                .ForEach(categoryViewData =>
                {
                    categoryViewData.ListTransaction = getTransactionForCategoryViewData(categoryViewData);
                    categoryViewData.Balance = categoryViewData.ListTransaction.Sum(tvd => tvd.Balance);
                    ListeCategory.Add(categoryViewData);
                });
        }

        private List<TransactionViewData> getTransactionForCategoryViewData(CategoryViewData categorieViewData)
        {
            var resultatFavoriteTransaction = TransactionService.GetTransactionsByCategoryId(categorieViewData.Id);


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