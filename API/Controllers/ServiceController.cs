﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using System.Security.Claims;
using System.Text;
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
        public IActionResult GetByVendorId(int Id)
        {
            return Ok(ServiceManager.Get().Where(s => s.VendorID == Id));
        }
        [Authorize(Roles = "vendor")]
        public IActionResult Add(CreateServiceViewModel Data)
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
            int LoggedInVendorId = VendorManager.GetVendorIdByUserId
                (UserId);
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
        [Authorize(Roles = "vendor,admin")]
        public IActionResult Update(UpdateServiceViewModel Data)
        {
            string LoggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            int? ServiceVendorID = ServiceManager.Get(Data.Id)!.VendorID;
            int LoggedInVendorId = VendorManager.GetVendorIdByUserId
                (LoggedInUserId);
            if (ServiceVendorID == null) return BadRequest("Service ID Invalid");
            else if (ServiceVendorID != LoggedInVendorId) return Unauthorized();
            else
            {
                Service Service = ServiceManager.Get(Data.Id)!;
                Service.Title = Data.Title ?? Service.Title;
                Service.Description = Data.Description ?? Service.Description;
                Service.Price = Data.Price ?? Service.Price;
                ServiceManager.Update(Service);
                return Ok("Service Updated Successfully");
            }
        }

            
            
    }
}


