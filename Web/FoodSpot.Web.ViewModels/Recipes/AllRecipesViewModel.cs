namespace FoodSpot.Web.ViewModels.Recipes
{
    using System.Collections.Generic;

    public class AllRecipesViewModel : RecipesPagingViewModel
    {
        public IEnumerable<RecipeViewModel> Recipes { get; set; }
    }
}
