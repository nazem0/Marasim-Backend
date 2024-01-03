using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.ReactDTOs
{
    public class CreateReactDTO
    {
        [Required]
        public required int PostId { get; set; }
    }
}

