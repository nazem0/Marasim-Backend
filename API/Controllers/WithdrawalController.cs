using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using Repository;
using ViewModels.PaymentViewModels;
using ViewModels.WithdrawalViewModels;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WithdrawalController : ControllerBase
    {
        private readonly WithdrawalManager WithdrawalManager;
        private readonly PaymentManager PaymentManager;
        private readonly VendorManager VendorManager;
        public WithdrawalController(
            WithdrawalManager _WithdrawalManager,
            PaymentManager _PaymentManager,
            VendorManager _VendorManager
            )
        {
            WithdrawalManager = _WithdrawalManager;
            PaymentManager = _PaymentManager;
            VendorManager = _VendorManager;
        }

        [HttpGet("Get")]
        public IActionResult Get(int PageSize = 2, int PageIndex = 1)
        {
            return Ok(WithdrawalManager.GetWithdrawals(PageSize, PageIndex));
        }

        [HttpPost("Add"), Authorize(Roles = "vendor")]
        public async Task<IActionResult> Add([FromForm] AddWithdrawlViewModel Data)
        {
            int? _vendorId = VendorManager.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (_vendorId is null) return Unauthorized();
            int VendorId = (int)_vendorId;

            if (!ModelState.IsValid)
            {
                List<ModelError> Errors = new();
                foreach (var item in ModelState.Values)
                {
                    foreach (ModelError item1 in item.Errors)
                    {
                        Errors.Add(item1);
                    }
                }
                return BadRequest(Errors);
            }
            else
            {
                bool added = await WithdrawalManager.AddAsync(Data, VendorId);
                if (!added)
                    return BadRequest();
                else
                {
                    return Ok();
                }
            }
        }
    }
}

