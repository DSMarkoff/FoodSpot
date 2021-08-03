namespace FoodSpot.Web.ViewModels.Votes
{
    using System.ComponentModel.DataAnnotations;

    public class CommentInputModel
    {
        public int RecipeId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Text { get; set; }
    }
}
