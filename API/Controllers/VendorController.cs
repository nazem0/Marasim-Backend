using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository;
using System.Security.Claims;
using System.Text;
using ViewModels.PostViewModels;
using ViewModels.UserViewModels;
using ViewModels.VendorViewModels;

namespace Marasim_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly VendorManager VendorManager;
        public VendorController(VendorManager _VendorManager)
        {
            VendorManager = _VendorManager;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var Data = VendorManager.Get()
                .Include(v => v.User);
            return new JsonResult(Data);
        }

        [HttpGet("GetVendorByID/{VendorID}")]
        public IActionResult GetVendorByID(int VendorID)
        {
            //محتاجين واحدة بتعرض بقا المعلومات كاااااااااملة جواها سرفس وبوست وكلو كلو كلو
            var Data = VendorManager.Get(VendorID)
                .Include(v => v.User)
                //.Select(v => v.ToVendorViewModel(v.User))
                .FirstOrDefault();
            return new JsonResult(Data);
        }

        [HttpGet("GetVendorFullFull/{VendorID}")]
        public IActionResult GetVendorFullFull(int VendorID)
        {
            var Data = VendorManager.Get(VendorID)
                .Include(v => v.User)
                .Include(v=> v.Services)
                .ThenInclude(s => s.ServiceAttachments)
                .Include(v => v.Posts)
                .ThenInclude(p => p.PostAttachments)
                .Include(v => v.Category)
                .Select(v => v.ToVendorFullViewModel(v.User))
                .FirstOrDefault();
            return new JsonResult(Data);
        }

    }
}

