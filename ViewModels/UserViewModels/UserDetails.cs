using System;
using Microsoft.AspNetCore.Http;

namespace ViewModels.UserViewModels
{
	public class UserDetails
	{
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string PicUrl { get; set; }
        public bool Gender { get; set; }
        public required string NationalID { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
    }
}

