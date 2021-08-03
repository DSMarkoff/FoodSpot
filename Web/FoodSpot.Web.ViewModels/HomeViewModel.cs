namespace FoodSpot.Web.ViewModels.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class HomeViewModel
    {
        public string Name { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
    }
}
