using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using System.Security.Claims;
using ViewModels.GenerationViewModels;
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

        [HttpGet("Count")]
        public IActionResult Count()
        {
            return Ok(VendorManager.Count());
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll(int PageSize = 5, int PageIndex = 1)
        {
            var Data = VendorManager.GetAll(PageSize, PageIndex);
            return Ok(Data);
        }

        [HttpGet("GetVendorById/{VendorId}")]
        public IActionResult GetVendorById(int VendorId)
        {
            Vendor? Data = VendorManager.Get(VendorId);
            if (Data == null)
                return NotFound();
            return Ok(Data.ToVendorViewModel(Data.User));
        }

        [HttpGet("GetVendorByUserId/{UserId}")]
        public IActionResult GetVendorByUserId(string UserId)
        {
            var Data = VendorManager.GetVendorByUserId(UserId);
            if (Data is null) return NotFound();
            return Ok(Data.ToVendorMidInfoViewModel());
        }

        [HttpGet("GetIntOfVendors/{NumOfVen}")]
        public IActionResult GetIntOfVendors(int NumOfVen)
        {
            if (NumOfVen / 1 != NumOfVen) return BadRequest();

            var Data = VendorManager.GetIntOfVendors(NumOfVen);
            return Ok(Data);
        }

        [HttpGet("GetVendorsMidInfo")]
        public IActionResult GetVendorsMidInfo()
        {
            var Data = VendorManager.Get().Select(v => v.ToVendorMidInfoViewModel());
            return Ok(Data);
        }

        [HttpGet("GetVendorFullFull/{VendorId}")]
        public async Task<IActionResult> GetVendorFullFull(int VendorId)
        {
            string? VendorUserId = VendorManager.GetUserIdByVendorId(VendorId);
            if (VendorUserId == null)
                return NotFound();

            User? VendorUser = await UserManager.FindByIdAsync(VendorUserId);
            if (VendorUser == null)
                return NotFound();

            VendorFullViewModel? Data = VendorManager.Get(VendorId)?.ToVendorFullViewModel(VendorUser);
            if (Data == null) return NotFound();
            return Ok(Data);
        }

        [HttpPut("Update")]
        [Authorize(Roles = "vendor")]
        public IActionResult Update([FromForm] UpdateVendorProfileViewModel Data)
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            VendorManager.Update(Data, UserId);
            return Ok();
        }
        [HttpPost("GenerateVendor"), Authorize]
        public IActionResult GenerateVendor(GenerateVendorViewModel Data)
        {
            return Ok(VendorManager.GenerateVendorAsync(Data));
        }

        [HttpPost("GeneratePackage")]
        public async Task<IActionResult> GeneratePackage(GeneratePackageViewModel Data)
        {
            return Ok(await VendorManager.GeneratePackage(Data));
        }

        [HttpGet("Filter/{PageIndex}")]
        public IActionResult Filter
            (int PageIndex,
            int PageSize = 4,
            string? Categories = null,
            int? GovernorateId = null,
            int? CityId = null,
            string? Name = null,
            string? District = null,
            int? Rate = null
            )
        {

            return Ok(VendorManager.Filter(new VendorFilterDTO
            {
                Categories = Categories,
                GovernorateId = GovernorateId,
                CityId = CityId,
                Name = Name,
                District = District,
                Rate = Rate
            },
            PageIndex,
            PageSize));
        }
    }
}

