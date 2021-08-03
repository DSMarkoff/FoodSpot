namespace FoodSpot.Web.ViewModels.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FoodSpot.Data.Models;
    using FoodSpot.Services.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class RecipeEditModel : IMapFrom<Recipe>
    {
        [Required]
        [MinLength(4)]
        public string Name { get; set; }

        [Required]
        [MinLength(100)]
        public string Instructions { get; set; }

        [Required]
        [Range(1, 24 * 60)]
        [Display(Name = "Time for preparation")]
        public int PreparationTime { get; set; }

        [Required]
        [Range(1, 24 * 60)]
        [Display(Name = "Time for cooking")]
        public int CookingTime { get; set; }

        [Required]
        [Range(1, 100)]
        [Display(Name = "Count of portions")]
        public int PortionsCount { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public IEnumerable<RecipeIngredientInputModel> Ingredients { get; set; }
    }
}
