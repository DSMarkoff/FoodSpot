namespace FoodSpot.Web.ViewModels.Administration.Recipes
{
    using System.Collections.Generic;

    using AutoMapper;
    using FoodSpot.Data.Models;
    using FoodSpot.Services.Mapping;
    using FoodSpot.Web.ViewModels.Recipes;

    public class ApproveRecipeViewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Instructions { get; set; }

        public int PreparationTime { get; set; }

        public int CookingTime { get; set; }

        public int PortionsCount { get; set; }

        public string Image { get; set; }

        public string UserUserName { get; set; }

        public string CategoryImage { get; set; }

        public string CreatedOn { get; set; }

        public IEnumerable<IngredientViewModel> RecipeIngredients { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, ApproveRecipeViewModel>()
                .ForMember(x => x.CreatedOn, opt =>
                    opt.MapFrom(x => x.CreatedOn.ToString("dd/MM/yyyy")));
        }
    }
}
