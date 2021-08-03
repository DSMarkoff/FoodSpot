namespace FoodSpot.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using FoodSpot.Data.Common.Repositories;
    using FoodSpot.Data.Models;
    using FoodSpot.Services.Mapping;

    public class IngredientsService : IIngredientsService
    {
        private readonly IDeletableEntityRepository<Ingredient> ingredientRepository;

        public IngredientsService(IDeletableEntityRepository<Ingredient> ingredientRepository)
        {
            this.ingredientRepository = ingredientRepository;
        }

        public IEnumerable<T> All<T>()
        {
           return this.ingredientRepository.All().To<T>().ToList();
        }
    }
}
