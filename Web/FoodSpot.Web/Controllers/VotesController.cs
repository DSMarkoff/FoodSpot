namespace FoodSpot.Web.Controllers
{
    using System.Threading.Tasks;

    using FoodSpot.Data.Models;
    using FoodSpot.Services.Data;
    using FoodSpot.Web.ViewModels.Votes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : ControllerBase
    {
        private readonly IVotesService votesService;
        private readonly UserManager<ApplicationUser> userManager;

        public VotesController(
            IVotesService votesService,
            UserManager<ApplicationUser> userManager)
        {
            this.votesService = votesService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<VoteViewModel>> Post(VoteInputModel model)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.votesService.SetVoteAsync(model, user.Id);

            return new VoteViewModel
            {
                AverageVote = this.votesService.GetAverageVote(model.RecipeId),
            };
        }
    }
}
