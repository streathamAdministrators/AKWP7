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
                                    where t.id == categoryId
                                    select t).First();

                    if (category != null)
                    {
                        datacontext.Category.DeleteOnSubmit(category);

                        datacontext.SubmitChanges();
                    }

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
                    var category = datacontext.Category.Where(a => a.id == categoryId).First();


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

                    var AddedCategory = datacontext.Category.Where(a => a.CreatedDate == newEntity.CreatedDate).First();

                    result.Value = AddedCategory.ToCategoryDto();

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



                    var entityToUpdate = datacontext.Category.Where(t => t.id == UpdatedEntity.id).First();

                    entityToUpdate.id = UpdatedEntity.id;
                    entityToUpdate.Name = UpdatedEntity.Name;
                    entityToUpdate.Color = UpdatedEntity.Color;
                    entityToUpdate.Balance = UpdatedEntity.Balance;
                    entityToUpdate.CreatedDate = DateTime.Now;


                    datacontext.SubmitChanges();

                    result.Value = entityToUpdate.ToCategoryDto();
                }

            }, () => "error");
        }


        public Result<List<CategoryDto>> GetAllCategories()
        {
            return Result<List<CategoryDto>>.SafeExecute<CategoryService>(result =>
            {
                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    var categories = (from t in datacontext.Category
                                      select t).ToList();

                    var listeDto = new List<CategoryDto>();

                    categories.ForEach(category => listeDto.Add(category.ToCategoryDto()));

                    result.Value = listeDto;
                }
            }, () => "error");
        }
    }
}
