namespace FoodSpot.Services.Data
{
    using System.Threading.Tasks;

    using FoodSpot.Web.ViewModels.Votes;

    public interface ICommentsService
    {
        Task CreateAsync(CommentInputModel model, string userId);
    }
}
