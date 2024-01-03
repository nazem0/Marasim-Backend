using Application.DTOs.ServiceAttachmentsDTOs;
using Application.Interfaces.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Marasim_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceAttachmentController : ControllerBase
    {
        private readonly IServiceAttachmentRepository _serviceAttachmentRepository;
        private readonly IVendorRepository _vendorRepository;
        public ServiceAttachmentController(IServiceAttachmentRepository serviceAttachmentRepository, IVendorRepository vendorRepository)
        {
            _serviceAttachmentRepository = serviceAttachmentRepository;
            _vendorRepository = vendorRepository;
        }

        [HttpGet("GetAllActive")]
        public IActionResult GetAllActive()
        {
            var Data = _serviceAttachmentRepository.GetAllActive();
            return new JsonResult(Data);
        }

        [HttpGet("GetByServiceId/{ServiceId}")]
        public IActionResult GetByServiceId(int ServiceId)
        {
            return Ok(_serviceAttachmentRepository.GetByServiceId(ServiceId));
        }

        [HttpGet("GetByVendorId/{VendorId}")]
        public IActionResult GetByVendorId(int VendorId)
        {
            var Data = _serviceAttachmentRepository.GetByVendorId(VendorId);
            return new JsonResult(Data);
        }

        [HttpGet("GetAllCustom")]
        public IActionResult GetAllCustom()
        {
            var Data = _serviceAttachmentRepository.GetCustomAttachment();
            return new JsonResult(Data);
        }
        [HttpPost("Add"), Authorize(Roles = "vendor")]
        public IActionResult Add([FromForm] CreateServiceAttachmentDTO Data)
        {
            int VendorId = _vendorRepository.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!)!;
            var result = _serviceAttachmentRepository.Add(Data, VendorId);
            if (result != HttpStatusCode.OK) return BadRequest();
            return Ok();
        }
        [HttpDelete("Delete/{Id}"), Authorize(Roles = "vendor")]
        public IActionResult Delete(int Id)
        {
            int vendorId = _vendorRepository.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = _serviceAttachmentRepository.Delete(Id, vendorId);
            if (result != HttpStatusCode.OK) return BadRequest();
            return Ok();
        }
    }
}
