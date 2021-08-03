namespace FoodSpot.Data.Models
{
    using System.Collections.Generic;

    using FoodSpot.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Recipes = new HashSet<Recipe>();
        }

        public string Name { get; set; }

        public string Image { get; set; }

        public ICollection<Recipe> Recipes { get; set; }
    }
}
