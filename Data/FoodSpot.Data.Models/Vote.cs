namespace FoodSpot.Data.Models
{
    using FoodSpot.Data.Common.Models;

    public class Vote : BaseDeletableModel<int>
    {
        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int Value { get; set; }
    }
}
