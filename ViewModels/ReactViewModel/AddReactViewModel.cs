using System.ComponentModel.DataAnnotations;

namespace ViewModels.ReactViewModel
{
    public class AddReactViewModel
    {
        [Required]
        public required int PostID { get; set; }
    }
}

