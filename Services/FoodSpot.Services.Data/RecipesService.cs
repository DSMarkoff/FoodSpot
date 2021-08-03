namespace FoodSpot.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using FoodSpot.Data.Common.Repositories;
    using FoodSpot.Data.Models;
    using FoodSpot.Services.Mapping;
    using FoodSpot.Web.ViewModels.Recipes;

    public class RecipesService : IRecipesService
    {
        private readonly IDeletableEntityRepository<Recipe> recipesRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientRepository;

        public RecipesService(
            IDeletableEntityRepository<Recipe> recipesRepository,
            IDeletableEntityRepository<Ingredient> ingredientRepository)
        {
            this.recipesRepository = recipesRepository;
            this.ingredientRepository = ingredientRepository;
        }

        public IEnumerable<T> AllToList<T>(int page, int itemsPerPage)
        {
            var recipes = this.recipesRepository.AllAsNoTracking()
                .Where(x => x.IsApproved == true)
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();

            return recipes;
        }

        public IEnumerable<T> AllToAdminList<T>(int page, int itemsPerPage)
        {
            var recipes = this.recipesRepository.AllAsNoTrackingWithDeleted()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();

            return recipes;
        }

        public IEnumerable<T> ResultList<T>(string name, int categoryId)
        {
            if (string.IsNullOrEmpty(name))
            {
                return this.recipesRepository
                    .AllAsNoTracking()
                    .Where(r => r.CategoryId == categoryId && r.IsApproved == true)
                    .To<T>()
                    .ToList();
            }

            var words = name.Split(new char[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);

            var query = this.recipesRepository.AllAsNoTracking().AsQueryable();

            foreach (var word in words)
            {
                query = query.Where(r => r.Name.Contains(word));
            }

            query = query.Where(r => r.CategoryId == categoryId && r.IsApproved == true);

            return query.To<T>().ToList();
        }

        public async Task CreateAsync(RecipeInputModel model, string userId, string path)
        {
            var recipe = new Recipe
            {
                Name = model.Name,
                Instructions = model.Instructions,
                PreparationTime = model.PreparationTime,
                CookingTime = model.CookingTime,
                PortionsCount = model.PortionsCount,
                CategoryId = model.CategoryId,
                UserId = userId,
            };

            foreach (var modelIngredient in model.Ingredients)
            {
                var ingredient = this.ingredientRepository.All().FirstOrDefault(x => x.Name == modelIngredient.IngredientName);

                if (ingredient is null)
                {
                    ingredient = new Ingredient { Name = modelIngredient.IngredientName };
                }

                recipe.RecipeIngredients.Add(new RecipeIngredient
                {
                    Ingredient = ingredient,
                    Quantity = modelIngredient.Quantity,
                });
            }

            if (model.Image is not null)
            {
                var uploadsFolder = Path.Combine(path, "images");
                recipe.Image = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, recipe.Image);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                model.Image.CopyTo(fileStream);
            }

            await this.recipesRepository.AddAsync(recipe);
            await this.recipesRepository.SaveChangesAsync();
        }

        public T GetRecipeById<T>(int id)
        {
            return this.recipesRepository.AllAsNoTracking().Where(x => x.Id == id && x.IsApproved == true).To<T>().FirstOrDefault();
        }

        public T GetRecipeByIdAdmin<T>(int id)
        {
            return this.recipesRepository.AllAsNoTrackingWithDeleted().Where(x => x.Id == id).To<T>().FirstOrDefault();
        }

        public int RecipesCount()
        {
           return this.recipesRepository.AllAsNoTracking().Where(x => x.IsApproved).Count();
        }

        public int RecipesResultCount(IEnumerable<string> ingredients)
        {
            var query = this.recipesRepository.AllAsNoTracking().Where(x => x.IsApproved == true).AsQueryable();

            foreach (var ingredient in ingredients)
            {
                query = query
                    .Where(r =>
                        r.RecipeIngredients
                            .Any(i => i.Ingredient.Name == ingredient));
            }

            return query.Count();
        }

        public int RecipesCountAdmin()
        {
            return this.recipesRepository.AllAsNoTrackingWithDeleted().Count();
        }

        public async Task UpdateAsync(int id, RecipeEditModel model)
        {
            var recipe = this.recipesRepository.All().FirstOrDefault(x => x.Id == id);

            recipe.Name = model.Name;
            recipe.Instructions = model.Instructions;
            recipe.PreparationTime = model.PreparationTime;
            recipe.CookingTime = model.CookingTime;
            recipe.PortionsCount = model.PortionsCount;
            recipe.CategoryId = model.CategoryId;

            if (model.Ingredients is not null)
            {
                foreach (var modelIngredient in model.Ingredients)
                {
                    var ingredient = this.ingredientRepository.All().FirstOrDefault(x => x.Name == modelIngredient.IngredientName);

                    if (ingredient is null)
                    {
                        ingredient = new Ingredient { Name = modelIngredient.IngredientName };
                    }

                    recipe.RecipeIngredients.Add(new RecipeIngredient
                    {
                        Ingredient = ingredient,
                        Quantity = modelIngredient.Quantity,
                    });
                }
            }

            await this.recipesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var recipe = this.recipesRepository.All().FirstOrDefault(x => x.Id == id);
            this.recipesRepository.Delete(recipe);

            await this.recipesRepository.SaveChangesAsync();
        }

        public async Task ApproveAsync(int id)
        {
            var recipe = this.recipesRepository.AllWithDeleted().FirstOrDefault(x => x.Id == id);
            recipe.IsApproved = true;

            await this.recipesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> ResultList<T>(IEnumerable<string> ingredients, int part, int itemsPerPart)
        {
            var query = this.recipesRepository.AllAsNoTracking().Where(x => x.IsApproved == true).AsQueryable();

            foreach (var ingredient in ingredients)
            {
                query = query
                    .Where(r =>
                        r.RecipeIngredients
                            .Any(i => i.Ingredient.Name == ingredient));
            }

            return query
                .OrderByDescending(x => x.Id)
                .Skip((part - 1) * itemsPerPart)
                .Take(itemsPerPart)
                .To<T>()
                .ToList();
        }
    }
}
