using System;
using Microsoft.AspNetCore.Http;

namespace ViewModels.UserViewModels
{
	public class UserPublicDetails
	{
        public required string Name { get; set; }
        public required string PicUrl { get; set; }
    }
}

