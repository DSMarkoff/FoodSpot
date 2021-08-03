namespace FoodSpot.Web.ViewModels.Recipes
{
    using System.Collections.Generic;

    using AutoMapper;
    using FoodSpot.Data.Models;
    using FoodSpot.Services.Mapping;
    using FoodSpot.Web.ViewModels.Comments;

    public class SingleRecipeViewModel : IMapFrom<Recipe>, IHaveCustomMappings
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

        public double AverageVote { get; set; }

        public IEnumerable<IngredientViewModel> RecipeIngredients { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, SingleRecipeViewModel>()
                .ForMember(x => x.CreatedOn, opt =>
                    opt.MapFrom(x => x.CreatedOn.ToString("dd/MM/yyyy")));
        }
    }
}
