using Application.DTOs.WithdrawalDTOs;
using Application.Interfaces.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WithdrawalController : ControllerBase
    {
        private readonly IWithdrawalRepository _withdrawalRepository;
        private readonly IVendorRepository _vendorRepository;
        public WithdrawalController(
            IWithdrawalRepository _WithdrawalManager,
            IPaymentRepository _PaymentManager,
            IVendorRepository _VendorManager
            )
        {
            _withdrawalRepository = _WithdrawalManager;
            _vendorRepository = _VendorManager;
        }

        [HttpGet("Get/{PageIndex}"), Authorize(Roles = "admin")]
        public IActionResult Get(int PageIndex = 1, int PageSize = 2)
        {
            return Ok(_withdrawalRepository.GetWithdrawals(PageIndex, PageSize));
        }

        [HttpPost("Add"), Authorize(Roles = "vendor")]
        public async Task<IActionResult> Add([FromForm] CreateWithdrawalDTO Data)
        {
            int vendorId = _vendorRepository.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

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

            HttpStatusCode result = await _withdrawalRepository.AddAsync(Data, vendorId);
            if (result != HttpStatusCode.OK) return BadRequest();
            return Ok();

        }

        [HttpGet("Confirm/{WithdrawalId}"), Authorize(Roles = "admin")]
        public IActionResult Confirm(int WithdrawalId)
        {
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

            HttpStatusCode result = _withdrawalRepository.ConfirmWithdrawal(WithdrawalId);
            if (result != HttpStatusCode.OK) return BadRequest();
            return Ok();

        }
    }
}

