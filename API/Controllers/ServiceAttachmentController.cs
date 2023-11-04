﻿using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Marasim_Backend.Controllers
{
    public class ServiceAttachmentController : ControllerBase
    {
        private readonly ServiceAttachmentManager ServiceAttachmentManager;
        public ServiceAttachmentController(ServiceAttachmentManager _ServiceAttachmentManager)
        {
            ServiceAttachmentManager = _ServiceAttachmentManager;
        }
        public IActionResult GetById(int Id)
        {
            var x = ServiceAttachmentManager.Get().Where(sa => sa.ServiceID == Id);
            return new JsonResult(x);
        }
    }
}
