using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.CategoryDTOs
{
    public class CreateCategoryDTO
    {
        [Required, MaxLength(30, ErrorMessage = "Category Name Cannot Exceed 30 Characters")]
        public required string Name { get; set; }
    }
}
