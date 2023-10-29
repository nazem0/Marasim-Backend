using Microsoft.AspNetCore.Http;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.UserViewModels
{
    public class UpdateProfileViewModel
    {
        [Required(AllowEmptyStrings = true), StringLength(50, MinimumLength = 3)]
        public string? Name { get; set; }

        [Required(AllowEmptyStrings = true), StringLength(14, MinimumLength = 14)]
        public string? PicURL { get; set; } = string.Empty;
        public IFormFile? Picture { get; set; }

        [Required(AllowEmptyStrings = true), StringLength(15, MinimumLength = 11)]
        public string? PhoneNumber { get; set; }
    }
}
