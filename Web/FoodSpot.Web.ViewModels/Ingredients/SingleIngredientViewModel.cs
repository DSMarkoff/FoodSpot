namespace FoodSpot.Web.ViewModels.Ingredients
{
    using FoodSpot.Data.Models;
    using FoodSpot.Services.Mapping;

    public class SingleIngredientViewModel : IMapFrom<Ingredient>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
