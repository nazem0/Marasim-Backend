using Application.DTOs.VendorDTOs;
using Application.DTOs.VendorGenerationDTOs;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Marasim_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorRepository _vendorRepository;
        private readonly UserManager<User> _userManager;

        public VendorController(
            IVendorRepository vendorRepository,
            UserManager<User> userManager)
        {
            _vendorRepository = vendorRepository;
            _userManager = userManager;
        }

        [HttpGet("Count")]
        public IActionResult Count()
        {
            return Ok(_vendorRepository.Count());
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll(int PageIndex = 1, int PageSize = 5)
        {
            var Data = _vendorRepository.GetAll(PageIndex, PageSize);
            return Ok(Data);
        }

        [HttpGet("GetVendorById/{VendorId}")]
        public IActionResult GetVendorById(int VendorId)
        {
            var vendor = _vendorRepository.GetVendorById(VendorId);
            if (vendor is null) return NotFound();
            else return Ok(vendor);
        }

        [HttpGet("GetVendorByUserId/{UserId}")]
        public IActionResult GetVendorByUserId(string UserId)
        {
            var vendor = _vendorRepository.GetVendorMidInfoByUserId(UserId);
            if (vendor is null) return NotFound();
            else return Ok(vendor);
        }

        [HttpGet("GetIntOfVendors/{NumOfVen}")]
        public IActionResult GetIntOfVendors(int NumOfVen)
        {
            if (NumOfVen / 1 != NumOfVen) return BadRequest();

            var Data = _vendorRepository.GetIntOfVendors(NumOfVen);
            return Ok(Data);
        }

        [HttpGet("GetVendorsMidInfo")]
        public async Task<IActionResult> GetVendorsMidInfoAsync()
        {
            var vendors = await _vendorRepository.GetVendorsMidInfoAsync();
            return Ok(vendors);
        }

        [HttpGet("GetVendorFullInfo/{VendorId}")]
        public IActionResult GetVendorFullInfo(int VendorId)
        {
            var vendor = _vendorRepository.GetVendorFullInfoByUserId(VendorId);
            return Ok(vendor);
        }

        [HttpPut("Update")]
        [Authorize(Roles = "vendor")]
        public IActionResult Update([FromForm] UpdateVendorDTO Data)
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            _vendorRepository.Update(Data, UserId);
            return Ok();
        }
        [HttpPost("GenerateVendor"), Authorize]
        public IActionResult GenerateVendor(GenerateVendorDTO Data)
        {
            return Ok(_vendorRepository.GenerateVendor(Data));
        }

        [HttpPost("GeneratePackage")]
        public async Task<IActionResult> GeneratePackageAsync(GeneratePackageDTO Data)
        {
            return Ok(await _vendorRepository.GeneratePackageAsync(Data));
        }

        [HttpGet("Filter/{PageIndex}")]
        public async Task<IActionResult> FilterAsync
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
            var Vendors = await _vendorRepository.FilterAsync(new VendorFilterCriteria
            {
                Categories = Categories,
                GovernorateId = GovernorateId,
                CityId = CityId,
                Name = Name,
                District = District,
                Rate = Rate
            },
            PageIndex,
            PageSize);
            return Ok(Vendors);
        }
    }
}

