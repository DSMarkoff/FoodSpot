namespace FoodSpot.Web.ViewModels.Recipes
{
    using FoodSpot.Data.Models;
    using FoodSpot.Services.Mapping;

    public class IngredientViewModel : IMapFrom<RecipeIngredient>
    {
        public string IngredientName { get; set; }

        public string Quantity { get; set; }
    }
}