using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        public IActionResult Index()
        {
            var x = ServiceManager.Get().ToList();
            return new JsonResult(x);
        }
        [Authorize(Roles = "vendor")]
        public IActionResult Create(CreateServiceViewModel Data)
        {
            int VendorID = VendorManager.GetVendorIdByUserId
                (User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            Service? CreatedService =
                ServiceManager.Add(Data.ToModel(VendorID)).Entity;
                ServiceManager.Save();
            foreach (FormFile item in Data.Pictures)
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
    }
}


