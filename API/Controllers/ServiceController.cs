using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using System.Security.Claims;
using ViewModels.ServiceViewModels;

namespace Marasim_Backend.Controllers
{
    public class ServiceController : ControllerBase
    {
        private ServiceManager ServiceManager { get; set; }
        private ServiceAttachmentManager ServiceAttachmentManager { get; set; }
        private VendorManager VendorManager { get; set; }
        public ServiceController
            (ServiceManager _serviceManager,
            ServiceAttachmentManager _ServiceAttachmentManager,
            VendorManager _vendorManager)
        {
            ServiceManager = _serviceManager;
            VendorManager = _vendorManager;
            ServiceAttachmentManager = _ServiceAttachmentManager;
        }
        public IActionResult GetAll()
        {
            var x = ServiceManager.Get();
            return Ok(x);
        }
        public IActionResult GetAllActive()
        {
            return Ok(ServiceManager.GetActive());
        }
        public IActionResult GetById(int Id)
        {
            var x = ServiceManager.Get(Id);
            return new JsonResult(x);
        }
        [Authorize(Roles = "vendor")]
        public IActionResult Add(CreateServiceViewModel Data)
        {
            var x = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            int LoggedInVendorId = VendorManager.GetVendorIdByUserId
                (x);
            Service? CreatedService =
                ServiceManager.Add(Data.ToModel(LoggedInVendorId)).Entity;
            ServiceManager.Save();
            foreach (IFormFile item in Data.Pictures)
            {
                FileInfo fi = new(item.FileName);
                string FileName = DateTime.Now.Ticks + fi.Extension;
                Helper.UploadMediaAsync
                    (User.FindFirstValue(ClaimTypes.NameIdentifier)!
                    , "ServiceAttachment", FileName, item, $"{CreatedService.ID}-{CreatedService.Title}");
                ServiceAttachmentManager.Add(
                    new ServiceAttachment
                    {
                        Resource = FileName,
                        Service = CreatedService
                    }
                    );
            }
            ServiceAttachmentManager.Save();
            return Ok();
        }
        [Authorize(Roles = "vendor")]
        public IActionResult Delete(int Id)
        {
            int? ServiceVendorID = ServiceManager.Get(Id)!.VendorID;
            int LoggedInVendorId = VendorManager.GetVendorIdByUserId
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
    }
}


