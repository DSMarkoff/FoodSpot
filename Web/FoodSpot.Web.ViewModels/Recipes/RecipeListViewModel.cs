namespace FoodSpot.Web.ViewModels.Recipes
{
    using System.Collections.Generic;

    public class RecipeListViewModel
    {
        public IEnumerable<RecipeViewModel> Recipes { get; set; }

        public int TotalParts { get; set; }

        public int TotalRecipes { get; set; }
    }
}
