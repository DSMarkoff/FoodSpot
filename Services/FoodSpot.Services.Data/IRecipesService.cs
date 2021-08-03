namespace FoodSpot.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FoodSpot.Web.ViewModels.Recipes;

    public interface IRecipesService
    {
        Task CreateAsync(RecipeInputModel model, string userId, string path);

        Task UpdateAsync(int id, RecipeEditModel model);

        Task DeleteAsync(int id);

        Task ApproveAsync(int id);

        IEnumerable<T> AllToList<T>(int page, int itemsPerPage);

        IEnumerable<T> AllToAdminList<T>(int page, int itemsPerPage);

        IEnumerable<T> ResultList<T>(string name, int categoryId);

        IEnumerable<T> ResultList<T>(IEnumerable<string> ingredients, int part, int itemsPerPart);

        int RecipesCount();

        int RecipesCountAdmin();

        int RecipesResultCount(IEnumerable<string> ingredients);

        T GetRecipeById<T>(int id);

        T GetRecipeByIdAdmin<T>(int id);
    }
}
