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
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(ServiceManager.Get()
                .Include(S => S.ServiceAttachments)
                .Include(S => S.Reservations)
                .Include(S => S.PromoCode)
                .Include(S => S.Reviews));
        }
        [HttpGet("GetAllActive")]
        public IActionResult GetAllActive()
        {
            return Ok(ServiceManager.GetActive()
                .Include(S => S.ServiceAttachments)
                .Include(S => S.Reservations)
                .Include(S => S.PromoCode)
                .Include(S => S.Reviews));
        }
        [HttpGet("GetById")]
        public IActionResult GetById(int Id)
        {
            var x = ServiceManager.Get(Id)
                .Include(S => S.ServiceAttachments)
                .Include(S => S.Reservations)
                .Include(S => S.PromoCode)
                .Include(S => S.Reviews);
            return new JsonResult(x);
        }
        [HttpGet("GetByVendorId/{Id}")]
        public IActionResult GetByVendorId(int Id)
        {
            return Ok(ServiceManager.Get()
                .Where(S => S.VendorId == Id && S.IsDeleted == false)
                .Include(S => S.ServiceAttachments)
                .Include(S => S.Reservations)
                .Include(S => S.PromoCode)
                .Include(S => S.Reviews)
                .Select(S => S.ToServiceViewModel(S.Vendor.UserId)));
        }
        [HttpPost("Add")]
        [Authorize(Roles = "vendor")]
        public IActionResult Add([FromForm] CreateServiceViewModel Data)
        {
            if (!ModelState.IsValId)
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
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            int LoggedInVendorId = VendorManager.GetVendorIdByUserId(UserId)!;
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
            int? ServiceVendorId = ServiceManager.Get(Id)!.FirstOrDefault()?.VendorId;
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
            int? ServiceVendorId = ServiceManager.Get(ServiceId)!.FirstOrDefault()?.VendorId;
            int? LoggedInVendorId = VendorManager.GetVendorIdByUserId
                (LoggedInUserId);
            if (ServiceVendorId == null) return BadRequest("Service Id InvalId");
            else if (ServiceVendorId != LoggedInVendorId) return Unauthorized();
            else
            {
                Service Service = ServiceManager.Get(ServiceId).FirstOrDefault()!;
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


