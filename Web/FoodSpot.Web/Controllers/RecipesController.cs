namespace FoodSpot.Web.Controllers
{
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FoodSpot.Data.Models;
    using FoodSpot.Services.Data;
    using FoodSpot.Services.Messaging;
    using FoodSpot.Web.ViewModels.Ingredients;
    using FoodSpot.Web.ViewModels.Recipes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class RecipesController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly IRecipesService recipesService;
        private readonly IVotesService votesService;
        private readonly IIngredientsService ingredientsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IEmailSender emailSender;

        public RecipesController(
            ICategoriesService categoriesService,
            IRecipesService recipesService,
            IVotesService votesService,
            IIngredientsService ingredientsService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment webHostEnvironment,
            IEmailSender emailSender)
        {
            this.categoriesService = categoriesService;
            this.recipesService = recipesService;
            this.votesService = votesService;
            this.ingredientsService = ingredientsService;
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
            this.emailSender = emailSender;
        }

        [HttpGet("Recipes/All/{id:int}")]
        public IActionResult All(int id)
        {
            var recipesCount = this.recipesService.RecipesCount();

            var itemsPerPage = 4;

            var viewModel = new AllRecipesViewModel
            {
                Recipes = this.recipesService.AllToList<RecipeViewModel>(id, itemsPerPage),
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

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new RecipeInputModel
            {
                Categories = this.categoriesService.GetCategoresAsSelectListItems(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(RecipeInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Categories = this.categoriesService.GetCategoresAsSelectListItems();

                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.recipesService.CreateAsync(model, user.Id, this.webHostEnvironment.WebRootPath);

            return this.RedirectToAction("All", new { id = 1 });
        }

        [HttpGet("Recipes/{id:int}")]
        public IActionResult Recipe(int id)
        {
            var model = this.recipesService.GetRecipeById<SingleRecipeViewModel>(id);

            if (model is null)
            {
                return this.NotFound();
            }

            model.AverageVote = this.votesService.GetAverageVote(id);

            return this.View(model);
        }

        [HttpGet("Recipes/Edit/{id:int}")]
        [Authorize]
        public IActionResult Edit(int id)
        {
            var model = this.recipesService.GetRecipeById<RecipeEditModel>(id);

            model.Categories = this.categoriesService.GetCategoresAsSelectListItems();

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await this.recipesService.DeleteAsync(id);

            return this.RedirectToAction("All", new { id = 1 });
        }

        public IActionResult Search()
        {
            var model = new IngredientListViewModel
            {
                Ingredients =
                    this.ingredientsService.All<SingleIngredientViewModel>().
                        OrderBy(x => x.Name),
            };

            return this.View(model);
        }

        public IActionResult Result(IngredientsListInputModel input)
        {
            if (input.Ingredients is null)
            {
                return this.RedirectToAction("Search");
            }

            var recipesCount = this.recipesService.RecipesResultCount(input.Ingredients);

            var part = 1;

            var itemsPerPart = 4;

            var viewModel = new RecipeListViewModel
            {
                Recipes =
                    this.recipesService.ResultList<RecipeViewModel>(input.Ingredients, part, itemsPerPart),
                TotalParts = recipesCount % itemsPerPart == 0 ?
                            recipesCount / itemsPerPart :
                            (recipesCount / itemsPerPart) + 1,
                TotalRecipes = recipesCount,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SendToEmail(int id)
        {
            var recipe = this.recipesService.GetRecipeById<SingleRecipeViewModel>(id);
            var html = new StringBuilder();
            html.AppendLine($"<h1>{recipe.Name}</h1>");
            html.AppendLine($"<h3>{recipe.UserUserName}</h3>");

            await this.emailSender.SendEmailAsync(
                "dsmarkov1@gmail.com",
                "FoodSpot",
                "siyoy31774@zcai77.com",
                recipe.Name,
                html.ToString());

            return this.RedirectToAction(nameof(this.Recipe), new { id });
        }

        public IActionResult LoadMore(Part part)
        {
            var itemsPerPart = 4;

            var viewModel = new RecipeListViewModel
            {
                Recipes =
                    this.recipesService.ResultList<RecipeViewModel>(part.Ingredients, part.Value, itemsPerPart),                
            };

            return this.PartialView("_PartingPartial", viewModel);
        }
    }
}
