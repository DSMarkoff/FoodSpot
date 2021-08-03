namespace FoodSpot.Services.Data
{
    using System.Threading.Tasks;

    using FoodSpot.Web.ViewModels.Votes;

    public interface IVotesService
    {
        Task SetVoteAsync(VoteInputModel model, string userId);

        double GetAverageVote(int recipeId);
    }
}
