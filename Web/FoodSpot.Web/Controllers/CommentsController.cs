namespace FoodSpot.Web.Controllers
{
    using System.Threading.Tasks;

    using FoodSpot.Data.Models;
    using FoodSpot.Services.Data;
    using FoodSpot.Web.ViewModels.Votes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(
            ICommentsService commentsService,
            UserManager<ApplicationUser> userManager)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CommentResponseModel>> Post(CommentInputModel model)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.commentsService.CreateAsync(model, user.Id);

            return new CommentResponseModel
            {
                Text = model.Text,
                UserUserName = user.UserName,
            };
        }
    }
}
