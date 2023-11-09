using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using ViewModels.UserViewModels;

namespace ViewModels.VendorViewModels
{
    public class UpdateVendorProfileViewModel
    {
        //[Required(AllowEmptyStrings = true), StringLength(50, MinimumLength = 3)]
        public string? Name { get; set; }

        public string? PicURL { get; set; }
        public IFormFile? Picture { get; set; }

        //[Required(AllowEmptyStrings = true), StringLength(15, MinimumLength = 11)]
        public string? PhoneNumber { get; set; }

        public int? CategoryId { get; set; }

        //[Required(AllowEmptyStrings = true), StringLength(1000, MinimumLength = 20)]
        public string? Summary { get; set; }
    }
}
