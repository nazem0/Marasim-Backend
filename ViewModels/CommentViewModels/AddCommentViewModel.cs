using System.ComponentModel.DataAnnotations;

namespace ViewModels.CommentViewModels
{
    public class AddCommentViewModel
    {
        [Required]
        public required int PostId { get; set; }
        [Required]
        public required string Text { get; set; }

    }
}
