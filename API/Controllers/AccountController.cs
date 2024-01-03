using Application.DTOs.AccountDTOs;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using System.Text;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IVendorRepository _vendorRepository;
        private readonly UserManager<User> _userManager;
        public AccountController(IAccountRepository accountRepository, IVendorRepository vendorRepository, UserManager<User> userManager)
        {
            _accountRepository = accountRepository;
            _vendorRepository = vendorRepository;
            _userManager = userManager;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] CustomerRegisterDTO customerRegisterDTO)
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
                return new ObjectResult(str);
            }

            IdentityResult result = await _accountRepository.Register(customerRegisterDTO);
            if (result.Succeeded) return Ok();
            else
            {
                var str2 = new StringBuilder();
                foreach (var item in result.Errors)
                {
                    str2.Append(item.Description);
                }
                return new ObjectResult(str2);
            }
        }
        [HttpPost("RegisterAsVendor")]
        public async Task<IActionResult> RegisterAsVendor([FromForm] VendorRegisterDTO vendorRegisterDTO)
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
                return new ObjectResult(Errors);
            }
            bool result = await _accountRepository.RegisterAsVendor(vendorRegisterDTO);
            if (result is true) return Ok();
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] LoginDTO loginDTO)
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

                return new ObjectResult(str);

            }
            SignInResult SignInResult = await _accountRepository.Login(loginDTO);
            if (SignInResult.IsLockedOut) return new ObjectResult("Your Account is Under Review");
            else if (SignInResult.Succeeded)
            {
                var User = await _userManager.FindByEmailAsync(loginDTO.Email);
                string tokenString = await _accountRepository.GenerateJSONWebToken(User!);
                IList<string> roles = await
                    _userManager.GetRolesAsync(User!);
                if (roles.Contains("vendor"))
                {
                    return Ok(new
                    {
                        token = tokenString,
                        role = roles,
                        profilePicture = User!.PicUrl,
                        name = User!.Name,
                        id = User!.Id,
                        vendorId = _vendorRepository.GetVendorIdByUserId(User.Id)
                    });
                }
                else
                {
                    return Ok(new
                    {
                        token = tokenString,
                        role = roles,
                        profilePicture = User!.PicUrl,
                        name = User!.Name,
                        id = User!.Id
                    });
                }
            }
            else return new ObjectResult("User name or Password is Wrong");

        }
        [HttpGet("Logout"), Authorize]
        public async Task Logout() => await _accountRepository.Logout();
        [HttpPut("ChangePassword"), Authorize]
        public async Task<IActionResult> ChangePasswordAsync([FromForm] ChangePasswordDTO changePasswordDTO)
        {
            if (!ModelState.IsValid)
            {
                var errorList = ModelState.SelectMany(ms => ms.Value!.Errors.Select(e => new { Field = ms.Key, Error = e.ErrorMessage })).ToList();
                return BadRequest(errorList);
            }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _accountRepository.ChangePassword(userId, changePasswordDTO);
            if (!result.Succeeded)
                return BadRequest(result.Errors.Select(e => new
                {
                    Field = e.Code,
                    Error = e.Description
                }).ToList());
            return Ok();
        }


    }
}

