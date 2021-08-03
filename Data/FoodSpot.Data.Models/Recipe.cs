namespace FoodSpot.Data.Models
{
    using System.Collections.Generic;

    using FoodSpot.Data.Common.Models;

    public class Recipe : BaseDeletableModel<int>
    {
        public Recipe()
        {
            this.RecipeIngredients = new HashSet<RecipeIngredient>();
            this.Votes = new HashSet<Vote>();
            this.Comments = new HashSet<Comment>();
        }

        public string Name { get; set; }

        public string Instructions { get; set; }

        public int PreparationTime { get; set; }

        public int CookingTime { get; set; }

        public int PortionsCount { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string Image { get; set; }

        public bool IsApproved { get; set; }

        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
