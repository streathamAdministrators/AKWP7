using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RMM.Data.Model;

namespace RMM.Business.CategoryService
{
    public interface ICategoryService
    {

        Result<CategoryEntity> DeleteCategorieById(int categoryId);

        Result<CategoryEntity> GetCategoryById(int categoryId, bool OnMinimal);

        Result<List<CategoryEntity>> GetAllCategories(bool OnMinimal);


        Result<CategoryEntity> CreateCategory(CreateCategoryCommand newCategoryCommand);

        Result<CategoryEntity> UpdateCategory(EditCategoryCommand editCategoryCommand);
    }
}
