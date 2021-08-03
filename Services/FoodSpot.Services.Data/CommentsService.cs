namespace FoodSpot.Services.Data
{
    using System.Threading.Tasks;

    using FoodSpot.Data.Common.Repositories;
    using FoodSpot.Data.Models;
    using FoodSpot.Web.ViewModels.Votes;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public CommentsService(IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public async Task CreateAsync(CommentInputModel model, string userId)
        {
            var comment = new Comment
            {
                Text = model.Text,
                RecipeId = model.RecipeId,
                UserId = userId,
            };

            await this.commentsRepository.AddAsync(comment);
            await this.commentsRepository.SaveChangesAsync();
        }
    }
}
