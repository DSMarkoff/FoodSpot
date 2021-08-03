namespace FoodSpot.Web.ViewModels.Ingredients
{
    using System.Collections.Generic;

    public class IngredientListViewModel
    {
        public IEnumerable<SingleIngredientViewModel> Ingredients { get; set; }
    }
}
