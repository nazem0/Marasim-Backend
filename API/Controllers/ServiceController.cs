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
        private VendorManager VendorManager { get; set; }
        public ServiceController
            (ServiceManager _ServiceManager,
            VendorManager vendorManager)
        {
            ServiceManager = _ServiceManager;
            VendorManager = vendorManager;
        }
        public IActionResult Index()
        {
            var x = ServiceManager.Get().ToList();
            return new JsonResult(x);
        }
        [Authorize(Roles = "Vendor")]
        public IActionResult CreateAsync(CreateServiceViewModel Data)
        {
            int VendorID = VendorManager.GetVendorByUserID
                (User.FindFirstValue(ClaimTypes.NameIdentifier)!).ID;
            Service? CreatedService =
                ServiceManager.Add(Data.ToModel(VendorID)).Entity;
            foreach (var item in Data.Pictures)
            {
                FileInfo fi = new(item.FileName);
                string FileName = DateTime.Now.Ticks + fi.Extension;
                Helper.UploadMediaAsync
                    (User.FindFirstValue(ClaimTypes.NameIdentifier)!
                    , "ProfilePicture", FileName, item);
            }
            return Ok(CreatedService);
        }
    }
}


