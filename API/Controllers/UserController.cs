using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
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

        public async Task<IActionResult> GetUserPublicDetails(string UserID)
        {
            var user = await UserManager.FindByIdAsync(UserID);
            return new JsonResult(user!.ToUserViewModel());
        }

        public async Task<IActionResult> Update(UpdateProfileViewModel Data)
        {
            ClaimsPrincipal? UserClaims = HttpContext.User;
            var User =  await UserManager.GetUserAsync(UserClaims);

            if (User == null) return new JsonResult("User Not On Our Database");
            if (Data.Picture == null) return new JsonResult("No Profile Picture Uploaded");

            Helper.DeleteMediaAsync(User.Id, "ProfilePicture", User.PicUrl);
            FileInfo fi = new(Data.Picture.FileName);
            string FileName = DateTime.Now.Ticks + fi.Extension;
            User.Name = Data.Name ?? User.Name;
            Helper.UploadMediaAsync(User.Id, "ProfilePicture", FileName, Data.Picture);
            Data.PicURL = FileName;

            User!.Name = Data.Name ?? User.Name;
            User.PicUrl = Data.PicURL ?? User.PicUrl;
            User.PhoneNumber = Data.PhoneNumber ?? User.PhoneNumber;
            await UserManager.UpdateAsync(User);

            return new JsonResult(User);
        }
    }
}
