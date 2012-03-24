using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RMM.Data;
using RMM.Business.ExtensionMethods;
using System.Data.Linq;

namespace RMM.Business.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private RmmDataContext datacontext = null;

        public Result<CategoryDto> DeleteCategorieById(int categoryId)
        {
            return Result<CategoryDto>.SafeExecute<CategoryService>(result =>
                {
                    using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                    {
                        var category = (from t in datacontext.Category
                                        where t.ID == categoryId
                                      select t).FirstOrDefault();

                        if (category != null)
                            datacontext.Category.DeleteOnSubmit(category);

                        datacontext.SubmitChanges();

                        result.Value = category.ToCategoryDto();
                    }

                },() => "error");
        }

        public Result<CategoryDto> GetCategoryById(int categoryId)
        {
            return Result<CategoryDto>.SafeExecute<CategoryService>(result =>
            {
                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    var category = (from t in datacontext.Category
                                    where t.ID == categoryId
                                    select t).FirstOrDefault();


                    result.Value = category.ToCategoryDto();
                }

            }, () => "error");
        }

        public Result<CategoryDto> CreateCategory(CategoryDto category)
        {
            return Result<CategoryDto>.SafeExecute<CategoryService>(result =>
            {
                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {

                    var newEntity = category.ToCategoryEntity();

                    datacontext.Category.InsertOnSubmit(newEntity);

                    datacontext.SubmitChanges();

                    result.Value = category;
                }

            }, () => "error");
        }

        public Result<CategoryDto> UpdateCategory(CategoryDto categoryToUpdate)
        {
            return Result<CategoryDto>.SafeExecute<CategoryService>(result =>
            {

                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {

                    //MAPPING
                    var UpdatedEntity = categoryToUpdate.ToCategoryEntity();



                    var entityToUpdate = datacontext.Category.Where(t => t.ID == UpdatedEntity.ID).FirstOrDefault();

                    entityToUpdate.ID = UpdatedEntity.ID;
                    entityToUpdate.Name = UpdatedEntity.Name;
                    entityToUpdate.Color = UpdatedEntity.Color;
                    entityToUpdate.Balance = UpdatedEntity.Balance;


                    datacontext.SubmitChanges();
                }

            }, () => "error");
        }


        public Result<List<CategoryDto>> GetAllCategories()
        {
            return Result<List<CategoryDto>>.SafeExecute<CategoryService>(result =>
            {

            }, () => "error");
        }
    }
}
