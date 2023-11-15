using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository;
using System.Security.Claims;
using System.Text;
using ViewModels.ServiceViewModels;

namespace Marasim_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly ServiceManager ServiceManager;
        private readonly ServiceAttachmentManager ServiceAttachmentManager;
        private readonly VendorManager VendorManager;
        public ServiceController
            (ServiceManager _serviceManager,
            ServiceAttachmentManager _ServiceAttachmentManager,
            VendorManager _vendorManager)
        {
            ServiceManager = _serviceManager;
            VendorManager = _vendorManager;
            ServiceAttachmentManager = _ServiceAttachmentManager;
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
        [HttpGet("GetById")]
        public IActionResult GetById(int Id)
        {
            var x = ServiceManager.Get(Id)?.ToServiceMinInfoViewModel();
            return Ok(x);
        }
        [HttpGet("GetByVendorId/{Id}")]
        public IActionResult GetByVendorId(int Id)
        {
            return Ok(ServiceManager.GetActive()
                .Where(S => S.VendorId == Id));
        }
        [HttpPost("Add")]
        [Authorize(Roles = "vendor")]
        public IActionResult Add([FromForm] CreateServiceViewModel Data)
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
            int? _loggedInVendorId = VendorManager.GetVendorIdByUserId(UserId);
            if (_loggedInVendorId is null) return NotFound();
            int LoggedInVendorId = (int)_loggedInVendorId!;
            Service? CreatedService =
                ServiceManager.Add(Data.ToModel(LoggedInVendorId)).Entity;
            ServiceManager.Save();
            foreach (IFormFile item in Data.Pictures)
            {
                FileInfo fi = new(item.FileName);
                string FileName = DateTime.Now.Ticks + fi.Extension;
                Helper.UploadMediaAsync
                    (User.FindFirstValue(ClaimTypes.NameIdentifier)!
                    , "ServiceAttachment", FileName, item, $"{CreatedService.Id}-{CreatedService.VendorId}");
                ServiceAttachmentManager.Add(
                    new ServiceAttachment
                    {
                        AttachmentUrl = FileName,
                        Service = CreatedService
                    }
                    );
            }
            ServiceAttachmentManager.Save();
            return Ok();
        }

        [HttpDelete("Delete/{Id}")]
        [Authorize(Roles = "vendor")]
        public IActionResult Delete(int Id)
        {
            int? ServiceVendorId = ServiceManager.Get(Id)!.VendorId;
            int? LoggedInVendorId = VendorManager.GetVendorIdByUserId
                (User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (ServiceVendorId != null && ServiceVendorId == LoggedInVendorId)
            {
                ServiceManager.Delete(Id);
                ServiceManager.Save();
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPut("Update/{ServiceId}")]
        [Authorize(Roles = "vendor,admin")]
        public IActionResult Update([FromForm] UpdateServiceViewModel Data, int ServiceId)
        {
            string LoggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            int? ServiceVendorId = ServiceManager.Get(ServiceId)!.VendorId;
            int? LoggedInVendorId = VendorManager.GetVendorIdByUserId
                (LoggedInUserId);
            if (ServiceVendorId == null) return BadRequest("Service Id Invalid");
            else if (ServiceVendorId != LoggedInVendorId) return Unauthorized();
            else
            {
                Service? Service = ServiceManager.Get(ServiceId);
                if (Service == null) return BadRequest();
                Service.Title = Data.Title ?? Service.Title;
                Service.Description = Data.Description ?? Service.Description;
                Service.Price = Data.Price ?? Service.Price;
                ServiceManager.Update(Service);
                ServiceManager.Save();
                return Ok();
            }
        }

    }
}


