using System.ComponentModel.DataAnnotations;

namespace ViewModels.CategoryViewModels
{
    public class AddCategoryViewModel
    {
        [Required, MaxLength(30, ErrorMessage = "Category Name Cannot Exceed 30 Characters")]
        public required string Name { get; set; }
    }
}
