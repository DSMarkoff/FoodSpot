using FoodSpot.Data.Common.Models;

namespace FoodSpot.Data.Models
{
    public class Comment : BaseDeletableModel<int>
    {
        public string Text { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }
    }
}
