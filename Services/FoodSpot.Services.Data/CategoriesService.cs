namespace FoodSpot.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using FoodSpot.Data.Common.Repositories;
    using FoodSpot.Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public IEnumerable<SelectListItem> GetCategoresAsSelectListItems()
        {
            return this.categoriesRepository.AllAsNoTracking().
                Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name,
                }).
                ToList();
        }
    }
}
