using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using System.Security.Claims;
using ViewModels.UserViewModels;

namespace Marasim_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager UserManager;
        public UserController(UserManager _UserManager)
        {
            UserManager = _UserManager;
        }
        [HttpGet("Users")]
        public IActionResult Index()
        {
            var x = UserManager.Users;
            return new JsonResult(x);
        }
        [HttpGet("UserDetails/{UserID}")]
        public async Task<IActionResult> UserDetails(string UserID)
        {
            User? user = await UserManager.FindByIdAsync(UserID);
            return new JsonResult(user!.ToUserViewModel());
        }
        [HttpPut("Update")]

        public async Task<IActionResult> Update(UpdateProfileViewModel Data)
        {
            ClaimsPrincipal? UserClaims = HttpContext.User;
            var User = await UserManager.GetUserAsync(UserClaims);

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
