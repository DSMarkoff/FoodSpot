namespace FoodSpot.Web.ViewModels.Recipes
{
    using AutoMapper;
    using FoodSpot.Data.Models;
    using FoodSpot.Services.Mapping;

    public class RecipeViewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string CategoryImage { get; set; }

        public string Name { get; set; }

        public string Instructions { get; set; }

        public string Image { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, RecipeViewModel>()
                .ForMember(x => x.Instructions, opt =>
                    opt.MapFrom(x => x.Instructions.Substring(0, 96) + "..."));
        }
    }
}
