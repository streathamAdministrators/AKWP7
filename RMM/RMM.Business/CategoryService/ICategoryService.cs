using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMM.Business.CategoryService
{
    public interface ICategoryService
    {
        //Renvoi l'element supprimé
        Result<CategoryDto> DeleteCategorieById(int categoryId);

        //Retournera un Dto
        Result<CategoryDto> GetCategoryById(int categoryId);

        Result<List<CategoryDto>> GetAllCategories();

        //Passage d'un dto en param apres
        Result<CategoryDto> CreateCategory(CategoryDto category);

        //Passage d'un dto en param apres
        Result<CategoryDto> UpdateCategory(CategoryDto categoryToUpdate);
    }
}
