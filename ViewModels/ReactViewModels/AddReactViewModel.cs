using System.ComponentModel.DataAnnotations;

namespace ViewModels.ReactViewModels
{
    public class AddReactViewModel
    {
        [Required]
        public required int PostID { get; set; }
    }
}

