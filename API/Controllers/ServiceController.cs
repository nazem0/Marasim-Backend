using Application.DTOs.ServiceDTOs;
using Application.Interfaces.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IVendorRepository _vendorRepository;
        public ServiceController
            (IServiceRepository serviceRepository,
            IVendorRepository vendorRepository)
        {
            _serviceRepository = serviceRepository;
            _vendorRepository = vendorRepository;
        }
        //[HttpGet("GetAll")]
        //public IActionResult GetAll()
        //{
        //    return Ok(ServiceManager.GetAll());
        //}
        //[HttpGet("GetAllActive")]
        //public IActionResult GetAllActive()
        //{
        //    return Ok(ServiceManager.GetActive());
        //}
        [HttpGet("GetById/{Id}")]
        public IActionResult GetById(int Id)
        {
            var service = _serviceRepository.GetById(Id);
            return Ok(service);
        }

        [HttpGet("GetByVendorId/{Id}")]
        public IActionResult GetByVendorId(int Id)
        {
            string? UserId = _vendorRepository.GetUserIdByVendorId(Id);
            if (UserId is null) return NotFound();
            return Ok(_serviceRepository.GetAllVendorServices(UserId));
        }
        [HttpPost("Add")]
        [Authorize(Roles = "vendor")]
        public IActionResult Add([FromForm] CreateServiceDTO Data)
        {
            if (!ModelState.IsValid)
            {
                var str = new StringBuilder();
                foreach (var item in ModelState.Values)
                {
                    foreach (var item1 in item.Errors)
                    {
                        str.Append(item1.ErrorMessage);
                    }
                }
                return BadRequest(str);

            }
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = _serviceRepository.Add(Data, UserId);
            if (result != HttpStatusCode.OK) return BadRequest();
            return Ok();
        }

        [HttpDelete("Delete/{Id}")]
        [Authorize(Roles = "vendor")]
        public IActionResult Delete(int Id)
        {
            int LoggedInVendorId = _vendorRepository.GetVendorIdByUserId
                (User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = _serviceRepository.Delete(Id, LoggedInVendorId);
            if (result != HttpStatusCode.OK) return BadRequest();
            return Ok();
        }

        [HttpPut("Update/{ServiceId}")]
        [Authorize(Roles = "vendor,admin")]
        public IActionResult Update([FromForm] UpdateServiceDTO updateServiceDTO)
        {
            string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            int vendorId = _vendorRepository.GetVendorIdByUserId(loggedInUserId);
            var result = _serviceRepository.Update(updateServiceDTO, vendorId);
            if (result != HttpStatusCode.OK) return BadRequest();
            return Ok();
        }

    }
}


