using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository;
using System.Security.Claims;
using System.Text;
using ViewModels.UserViewModels;
using ViewModels.VendorViewModels;

namespace Marasim_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly VendorManager VendorManager;
        private readonly UserManager UserManager;

        public VendorController(
            VendorManager _VendorManager,
            UserManager _UserManager)
        {
            VendorManager = _VendorManager;
            UserManager = _UserManager;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var Data = VendorManager.Get()
                .Include(v => v.User);
            return new JsonResult(Data);
        }

        [HttpGet("GetVendorByID/{VendorID}")]
        public IActionResult GetVendorByID(int VendorID)
        {
            var Data = VendorManager.Get(VendorID)
                .Include(v => v.User)
                //.Select(v => v.ToVendorViewModel(v.User))
                .FirstOrDefault();
            return new JsonResult(Data);
        }

        [HttpGet("GetVendorByUserId/{UserId}")]
        public IActionResult GetVendorByUserId(string UserId)
        {
            var Data = VendorManager.GetVendorByUserId(UserId);
            return new JsonResult(Data);
        }

        [HttpGet("GetVendorMidInfo")]
        public IActionResult GetVendorMidInfo()
        {
            var Data = VendorManager.Get()
                .Include(v => v.User)
                .Include(v => v.Category)
                .Select(v => v.ToVendorMidInfoViewModel());
            return new JsonResult(Data);
        }

        [HttpGet("GetVendorFullFull/{VendorID}")]
        public IActionResult GetVendorFullFull(int VendorID)
        {
            var Data = VendorManager.Get(VendorID)
                .Include(v => v.User)
                .Include(v => v.Services.Where(s => s.IsDeleted == false))
                .ThenInclude(s => s.ServiceAttachments)
                .Include(v => v.Posts)
                .ThenInclude(p => p.PostAttachments)
                .Include(v => v.Category)
                .Select(v => v.ToVendorFullViewModel(v.User))
                .FirstOrDefault();
            return new JsonResult(Data);
        }

        [HttpPut("Update")]
        [Authorize(Roles = "vendor")]
        public async Task<IActionResult> Update([FromForm] UpdateVendorProfileViewModel Data)
        {
            ClaimsPrincipal? UserClaims = HttpContext.User;
            var User = await UserManager.GetUserAsync(UserClaims);
            if (User == null)
            {
                return BadRequest("User Not On Our Database");
            }
            var Vendor = VendorManager.GetVendorByUserId(User!.Id);
            if (Data.Picture != null)
            {
                Helper.DeleteMediaAsync(User.Id, "ProfilePicture", User.PicUrl);
                FileInfo fi = new(Data.Picture.FileName);
                string FileName = DateTime.Now.Ticks + fi.Extension;
                User.Name = Data.Name ?? User.Name;
                Helper.UploadMediaAsync(User.Id, "ProfilePicture", FileName, Data.Picture);
                Data.PicURL = FileName;
            }

            User!.Name = Data.Name ?? User.Name;
            User.PicUrl = Data.PicURL ?? User.PicUrl;
            User.PhoneNumber = Data.PhoneNumber ?? User.PhoneNumber;
            Vendor.Summary = Data.Summary ?? Vendor.Summary;
            Vendor.CategoryId = Data.CategoryId ?? Vendor.CategoryId;

            VendorManager.Update(Vendor);
            VendorManager.Save();
            _ = await UserManager.UpdateAsync(User);
            return Ok();
        }

    }
}

