namespace FoodSpot.Data.Models
{
    using FoodSpot.Data.Common.Models;

    public class RecipeIngredient : BaseDeletableModel<int>
    {
        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        public int IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; }

        public string Quantity { get; set; }
    }
}
