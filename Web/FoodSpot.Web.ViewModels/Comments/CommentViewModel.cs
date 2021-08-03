namespace FoodSpot.Web.ViewModels.Comments
{
    using AutoMapper;
    using FoodSpot.Data.Models;
    using FoodSpot.Services.Mapping;

    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public string Text { get; set; }

        public string UserUserName { get; set; }

        public string CreatedOnDate { get; set; }

        public string CreatedOnTime { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                .ForMember(x => x.CreatedOnDate, opt =>
                    opt.MapFrom(x => x.CreatedOn.ToString("dd/MM/yyyy")))
                .ForMember(x => x.CreatedOnTime, opt =>
                    opt.MapFrom(x => x.CreatedOn.ToString("HH:mm")));
        }
    }
}
