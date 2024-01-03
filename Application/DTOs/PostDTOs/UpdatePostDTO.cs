using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.PostDTOs
{
    public class UpdatePostDTO
    {
        public int Id { get; set; }
        [StringLength(100, MinimumLength = 5)]
        public string? Title { get; set; }

        [StringLength(1000, MinimumLength = 10)]
        public string? Description { get; set; }
    }
}

