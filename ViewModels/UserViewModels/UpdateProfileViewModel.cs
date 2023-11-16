using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.UserViewModels
{
    public class UpdateProfileViewModel
    {
        [StringLength(50, MinimumLength = 3)]
        public string? Name { get; set; } = null;

        [StringLength(14, MinimumLength = 14)]
        public string? PicURL { get; set; } = null;
        public IFormFile? Picture { get; set; }

        [StringLength(15)]
        public string? PhoneNumber { get; set; } = null;
    }
}
