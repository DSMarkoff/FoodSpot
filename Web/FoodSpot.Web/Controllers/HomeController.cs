namespace FoodSpot.Web.Controllers
{
    using System.Diagnostics;

    using FoodSpot.Services.Data;
    using FoodSpot.Web.ViewModels;
    using FoodSpot.Web.ViewModels.Recipes;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IRecipesService recipesService;
        private readonly ICategoriesService categoriesService;

        public HomeController(
            IRecipesService recipesService,
            ICategoriesService categoriesService)
        {
            this.recipesService = recipesService;
            this.categoriesService = categoriesService;
        }

        public IActionResult Index()
        {
            var model = new HomeViewModel
            {
                Categories = this.categoriesService.GetCategoresAsSelectListItems(),
            };

            return this.View(model);
        }

        public IActionResult Result(HomeViewModel model)
        {
            var viewModel = new RecipeListViewModel
            {
                Recipes =
                    this.recipesService
                    .ResultList<RecipeViewModel>(model.Name, model.CategoryId),
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
