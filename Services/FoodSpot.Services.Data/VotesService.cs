namespace FoodSpot.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using FoodSpot.Data.Common.Repositories;
    using FoodSpot.Data.Models;
    using FoodSpot.Web.ViewModels.Votes;

    public class VotesService : IVotesService
    {
        private readonly IDeletableEntityRepository<Vote> votesRepository;

        public VotesService(IDeletableEntityRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public double GetAverageVote(int recipeId)
        {
            if (this.votesRepository.AllAsNoTracking().Where(x => x.RecipeId == recipeId).Count() == 0)
            {
                return 0;
            }

            return this.votesRepository.AllAsNoTracking().Where(x => x.RecipeId == recipeId).Average(x => x.Value);
        }

        public async Task SetVoteAsync(VoteInputModel model, string userId)
        {
            var vote = this.votesRepository.All().FirstOrDefault(x => x.RecipeId == model.RecipeId && x.UserId == userId);

            if (vote is null)
            {
                vote = new Vote
                {
                    RecipeId = model.RecipeId,
                    UserId = userId,
                    Value = model.Value,
                };

                await this.votesRepository.AddAsync(vote);
            }
            else
            {
                vote.Value = model.Value;
            }

            await this.votesRepository.SaveChangesAsync();
        }
    }
}
