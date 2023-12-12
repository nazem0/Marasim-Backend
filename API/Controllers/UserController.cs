using Microsoft.AspNetCore.Authorization;
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
        private readonly UserRepository UserManager;
        public UserController(UserRepository _UserManager)
        {
            UserManager = _UserManager;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(int PageSize = 5, int PageIndex = 1)
        {
            var Data = await UserManager.GetAll(PageSize, PageIndex);
            return Ok(Data);
        }

        [HttpGet("Count")]
        public async Task<IActionResult> Count()
        {
            return Ok(await UserManager.Count());
        }

        [HttpGet("UserDetails/{UserId}")]
        public async Task<IActionResult> UserDetails(string UserId)
        {
            User? User = await UserManager.FindByIdAsync(UserId);
            if (User is null) return NotFound();
            return Ok(User.ToUserViewModel());
        }

        [HttpPut("Update"), Authorize]
        public async Task<IActionResult> Update([FromForm] UpdateProfileViewModel Data)
        {
            ClaimsPrincipal? UserClaims = HttpContext.User;
            var User = await UserManager.GetUserAsync(UserClaims);

            if (User == null) return Unauthorized("User Not On Our Database");

            if (Data.Picture is not null)
            {
                Helper.DeleteMediaAsync(User.Id, "ProfilePicture", User.PicUrl);
                FileInfo fi = new(Data.Picture.FileName);
                string FileName = DateTime.Now.Ticks + fi.Extension;
                User.Name = Data.Name ?? User.Name;
                Helper.UploadMediaAsync(User.Id, "ProfilePicture", FileName, Data.Picture);
                Data.PicURL = FileName;
                User.PicUrl = Data.PicURL ?? User.PicUrl;
            }

            User.Name = Data.Name ?? User.Name;
            User.PhoneNumber = Data.PhoneNumber ?? User.PhoneNumber;
            await UserManager.UpdateAsync(User);

            return Ok();
        }
    }
}
