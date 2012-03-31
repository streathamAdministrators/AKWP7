using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RMM.Data.Model;

namespace RMM.Business.CategoryService
{
    public interface ICategoryService
    {

        /// <summary>
        /// Recupere une categorie par son Id. OnMinimal de type bool permet le eagger loading. si OnMinimal est a true,
        /// l'object sera dit "Minimal", la category sera donc en LazyLoad, est ne possedera pas sa liste de transactions.
        /// </summary>
        /// <param name="categoryId">Id de la category</param>
        /// <param name="OnMinimal">Chargement de la liste des transactions pour la category en question</param>
        /// <returns>la category recherchée</returns>
        Result<Category> GetCategoryById(int categoryId, bool OnMinimal);



        /// <summary>
        /// Recupere TOUTES les categories par son Id. OnMinimal de type bool permet le eagger loading. si OnMinimal est a true,
        /// l'object sera dit "Minimal", les categories seront donc en LazyLoad, est ne possederont pas leur liste de transactions.
        /// </summary>
        /// <param name="categoryId">Id de la category</param>
        /// <param name="OnMinimal">Chargement de la liste des transactions par category</param>
        /// <returns>liste des categories</returns>
        Result<List<Category>> GetAllCategories(bool OnMinimal);


        /// <summary>
        /// Creer une category par la commanc CreateCategory command.
        /// </summary>
        /// <param name="newCategoryCommand">commande permettant de faire la creation</param>
        /// <returns>Retour de l'objet crée</returns>
        Result<Category> CreateCategory(CreateCategoryCommand newCategoryCommand);


        /// <summary>
        /// Mise a jour de la category en question.
        /// </summary>
        /// <param name="editCategoryCommand">commande permettant de faire l'update</param>
        /// <returns>Retourne l'object mis à jour</returns>
        Result<Category> UpdateCategory(EditCategoryCommand editCategoryCommand);



        /// <summary>
        /// suppression de la category
        /// </summary>
        /// <param name="categoryId">Category à supprimer</param>
        /// <returns>Retourne la catégory supprimée pour un eventuel Undo..</returns>
        Result<Category> DeleteCategorieById(int categoryId);

    }
}
