namespace FoodSpot.Web.ViewModels.Administration.Recipes
{
    using System;

    using FoodSpot.Data.Models;
    using FoodSpot.Services.Mapping;

    public class RecipeViewModel : IMapFrom<Recipe>
    {
        public string Image { get; set; }

        public string Name { get; set; }

        public string UserUserName { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public bool IsApproved { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public int Id { get; set; }

        public string UserId { get; set; }
    }
}
