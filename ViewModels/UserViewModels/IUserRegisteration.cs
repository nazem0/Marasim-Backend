using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public interface IUserRegisteration
    {
        [Required, StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required, StringLength(14, MinimumLength = 14)]
        public string NationalID { get; set; }
        public string PicUrl { get; set; }
        [Required]
        public IFormFile Picture { get; set; }

        [Required, StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(15, MinimumLength = 11)]
        public string PhoneNumber { get; set; }

        [Required, StringLength(20, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, StringLength(20, MinimumLength = 8)]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        public bool Gender { get; set; }
        public bool IsDeleted { get; set; }


    }
}
