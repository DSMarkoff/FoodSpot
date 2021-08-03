namespace FoodSpot.Web.ViewModels.Administration.Recipes
{
    using System.Collections.Generic;

    using FoodSpot.Web.ViewModels.Recipes;

    public class IndexViewModel : RecipesPagingViewModel
    {
        public IEnumerable<RecipeViewModel> Recipes { get; set; }
    }
}
