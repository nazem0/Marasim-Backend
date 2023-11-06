using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository;
using System.Security.Claims;
using System.Text;
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
            var Data = VendorManager.Get(VendorID)
                .Include(v => v.User)
                .FirstOrDefault();
                //.Select(v => v.ToVendorViewModel(v.User))
                //.FirstOrDefault();
            return new JsonResult(Data);
        }

    }
}

