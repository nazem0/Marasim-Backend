using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.CommentDTOs
{
    public class CreateCommentDTO
    {
        [Required]
        public required int PostId { get; set; }
        [Required]
        public required string Text { get; set; }

    }
}
