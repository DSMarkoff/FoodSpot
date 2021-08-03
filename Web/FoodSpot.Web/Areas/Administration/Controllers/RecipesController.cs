namespace FoodSpot.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using FoodSpot.Services.Data;
    using FoodSpot.Web.ViewModels.Administration.Recipes;
    using FoodSpot.Web.ViewModels.Recipes;
    using Microsoft.AspNetCore.Mvc;

    using RecipeViewModel = FoodSpot.Web.ViewModels.Administration.Recipes.RecipeViewModel;

    public class RecipesController : AdministrationController
    {
        private readonly IRecipesService recipesService;
        private readonly ICategoriesService categoriesService;
        private readonly IVotesService votesService;

        public RecipesController(
            IRecipesService recipesService,
            ICategoriesService categoriesService,
            IVotesService votesService)
        {
            this.recipesService = recipesService;
            this.categoriesService = categoriesService;
            this.votesService = votesService;
        }

        [HttpGet("Administration/Recipes/Index/{id:int}")]
        public IActionResult Index(int id)
        {
            var recipesCount = this.recipesService.RecipesCountAdmin();

            var itemsPerPage = 6;

            var viewModel = new IndexViewModel
            {
                Recipes = this.recipesService.AllToAdminList<RecipeViewModel>(id, itemsPerPage),
                CurrentPage = id,
                RecipesCount = recipesCount,
                MaxPage = recipesCount % itemsPerPage == 0 ?
                            recipesCount / itemsPerPage :
                            (recipesCount / itemsPerPage) + 1,
            };

            if (id > viewModel.MaxPage)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        [HttpGet("Administration/Recipes/Approve/{id:int}")]
        public IActionResult Approve(int id)
        {
            var model = this.recipesService.GetRecipeByIdAdmin<ApproveRecipeViewModel>(id);

            if (model is null)
            {
                return this.NotFound();
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Approved(int id)
        {
            await this.recipesService.ApproveAsync(id);

            return this.RedirectToAction("Index", new { id = 1 });
        }

        [HttpGet("Administration/Recipes/{id:int}")]
        public IActionResult Recipe(int id)
        {
            var model = this.recipesService.GetRecipeByIdAdmin<SingleRecipeViewModel>(id);

            model.AverageVote = this.votesService.GetAverageVote(id);

            return this.View(model);
        }

        [HttpGet("Administration/Recipes/Edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var model = this.recipesService.GetRecipeByIdAdmin<RecipeEditModel>(id);

            model.Categories = this.categoriesService.GetCategoresAsSelectListItems();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, RecipeEditModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Categories = this.categoriesService.GetCategoresAsSelectListItems();

                return this.View(model);
            }

            await this.recipesService.UpdateAsync(id, model);

            return this.RedirectToAction("Recipe", new { id });
        }

        [HttpGet("Administration/Recipes/Delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            var model = this.recipesService.GetRecipeByIdAdmin<SingleRecipeViewModel>(id);

            model.AverageVote = this.votesService.GetAverageVote(id);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Deleted(int id)
        {
            await this.recipesService.DeleteAsync(id);

            return this.RedirectToAction("Index", new { id = 1 });
        }
    }
}
