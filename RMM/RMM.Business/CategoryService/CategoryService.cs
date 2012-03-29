using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RMM.Data;
using System.Data.Linq;
using RMM.Business.Helpers;
using RMM.Data.Model;
using System.Linq.Expressions;

namespace RMM.Business.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private RmmDataContext datacontext = null;

        public Result<Category> DeleteCategorieById(int categoryId)
        {
            return Result<Category>.SafeExecute<CategoryService>(result =>
                {
                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    var category = (from t in datacontext.Category
                                    where t.ID == categoryId
                                    select t).First();

                    if (category != null)
                    {
                        datacontext.Category.DeleteOnSubmit(category);

                        datacontext.SubmitChanges();
                    }

                    result.Value = category;
                    }

                },() => "error");
        }

        public Result<Category> GetCategoryById(int categoryId, bool OnMinimal)
        {
            return Result<Category>.SafeExecute<CategoryService>(result =>
            {
                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    if(!OnMinimal)
                    datacontext.LoadOptions = DBHelpers.GetConfigurationLoader<Category>(c => c.TransactionList);

                    var category = datacontext.Category.Where(a => a.ID == categoryId).First();


                    result.Value = category;
                }

            }, () => "error");
        }

        public Result<Category> CreateCategory(CreateCategoryCommand newCategoryCommand)
        {
            return Result<Category>.SafeExecute<CategoryService>(result =>
            {
                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    var newCategoryEntity = new Category()
                    {
                        Balance = 0,
                        Color = newCategoryCommand.Color,
                        CreatedDate = DateTime.Now,
                        Name = newCategoryCommand.Name
                    };
                        


                    datacontext.Category.InsertOnSubmit(newCategoryEntity);

                    datacontext.SubmitChanges();

                    var AddedCategory = datacontext.Category.Where(a => a.CreatedDate == newCategoryEntity.CreatedDate).First();

                    result.Value = AddedCategory;

                }

            }, () => "error");
        }

        public Result<Category> UpdateCategory(EditCategoryCommand editCategoryCommand)
        {
            return Result<Category>.SafeExecute<CategoryService>(result =>
            {

                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {

                    var entityToUpdate = datacontext.Category.Where(t => t.ID == editCategoryCommand.id).First();

                    entityToUpdate.ID = editCategoryCommand.id;
                    entityToUpdate.Name = editCategoryCommand.Name;
                    entityToUpdate.Color = editCategoryCommand.Color;
                    entityToUpdate.CreatedDate = DateTime.Now;


                    datacontext.SubmitChanges();

                    result.Value = entityToUpdate;
                }

            }, () => "error");
        }

        public Result<List<Category>> GetAllCategories(bool OnMinimal)
        {
            return Result<List<Category>>.SafeExecute<CategoryService>(result =>
            {
                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    if (!OnMinimal)
                    {
                        datacontext.LoadOptions = DBHelpers.GetConfigurationLoader<Category>(cat => cat.TransactionList);
                    }

                    var categories = datacontext.Category.ToList();


                    result.Value = categories;
                }
            }, () => "error");
        }
    }
}
