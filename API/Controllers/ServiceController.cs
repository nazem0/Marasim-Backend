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
        [HttpGet("GetByVendorId/{Id?}")]
        public IActionResult GetByVendorId(int Id)
        {
            return Ok(ServiceManager.Get()
                .Where(S => S.VendorID == Id)
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
                    , "ServiceAttachment", FileName, item, $"{CreatedService.Id}-{CreatedService.Title}");
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
        [HttpDelete("Delete")]
        [Authorize(Roles = "vendor")]
        public IActionResult Delete(int Id)
        {
            int? ServiceVendorID = ServiceManager.Get(Id)!.FirstOrDefault()?.VendorID;
            int? LoggedInVendorId = VendorManager.GetVendorIdByUserId
                (User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (ServiceVendorID != null && ServiceVendorID == LoggedInVendorId)
            {
                ServiceManager.Delete(Id);
                ServiceManager.Save();
                return Ok("Service Deleted Successfully");
            }
            else
            {
                return Unauthorized();
            }
        }
        [HttpPost("Update")]
        [Authorize(Roles = "vendor,admin")]
        public IActionResult Update(UpdateServiceViewModel Data)
        {
            string LoggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            int? ServiceVendorID = ServiceManager.Get(Data.Id)!.FirstOrDefault()?.VendorID;
            int? LoggedInVendorId = VendorManager.GetVendorIdByUserId
                (LoggedInUserId);
            if (ServiceVendorID == null) return BadRequest("Service ID Invalid");
            else if (ServiceVendorID != LoggedInVendorId) return Unauthorized();
            else
            {
                Service Service = ServiceManager.Get(Data.Id).FirstOrDefault()!;
                Service.Title = Data.Title ?? Service.Title;
                Service.Description = Data.Description ?? Service.Description;
                Service.Price = Data.Price ?? Service.Price;
                ServiceManager.Update(Service);
                return Ok("Service Updated Successfully");
            }
        }



    }
}


