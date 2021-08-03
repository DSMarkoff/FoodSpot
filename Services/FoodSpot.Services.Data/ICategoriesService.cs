namespace FoodSpot.Services.Data
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ICategoriesService
    {
        IEnumerable<SelectListItem> GetCategoresAsSelectListItems();
    }
}
