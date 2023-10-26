﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using System.Linq;
using System.Security.Claims;
using ViewModels.UserViewModels;

namespace Marasim_Backend.Controllers
{
    public class UserController : ControllerBase
    {
        private UserManager<User> UserManager { get; set; }
        public UserController(UserManager<User> _UserManager)
        {
            UserManager = _UserManager;
        }
        public IActionResult Index()
        {
            var x = UserManager.Users;
            return new JsonResult(x);
        }

        public async Task<IActionResult> UpdateAsync(UpdateProfileViewModel Data)
        {
            ClaimsPrincipal? UserClaims = HttpContext.User;
            var User = UserManager.GetUserAsync(UserClaims).Result;

            if (User != null)
            {
                if (Data.Picture != null)
                {
                    Helper.DeleteMediaAsync(User.Id, "ProfilePicture", User.PicUrl);
                    FileInfo fi = new(Data.Picture.FileName);
                    string FileName = DateTime.Now.Ticks + fi.Extension;
                    User.Name = Data.Name ?? User.Name;
                    Helper.UploadMediaAsync(User.Id, "ProfilePicture", FileName, Data.Picture);
                    Data.PicURL = FileName;
                }
                User.Name = Data.Name ?? User.Name;
                User.NationalID = Data.NationalID ?? User.NationalID;
                User.PicUrl = Data.PicURL ?? User.PicUrl;
                User.PhoneNumber = Data.PhoneNumber ?? User.PhoneNumber;
                await UserManager.UpdateAsync(User);
            }
            return new JsonResult(User);
        }
    }
}
