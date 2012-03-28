using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RMM.Data.Model;

namespace RMM.Business.CategoryService
{
    public interface ICategoryService
    {

        Result<Category> DeleteCategorieById(int categoryId);

        Result<Category> GetCategoryById(int categoryId, bool OnMinimal);

        Result<List<Category>> GetAllCategories(bool OnMinimal);


        Result<Category> CreateCategory(CreateCategoryCommand newCategoryCommand);

        Result<Category> UpdateCategory(EditCategoryCommand editCategoryCommand);
    }
}
