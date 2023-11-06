using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Models;
using Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ViewModels.UserViewModels;
using ViewModels.VendorViewModels;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountManager AccountManager;
        private readonly UserManager UserManager;
        private readonly VendorManager VendorManager;

        public AccountController(AccountManager _accManger, UserManager _userManager, VendorManager vendorManager)
        {
            AccountManager = _accManger;
            UserManager = _userManager;
            VendorManager = vendorManager;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] UserRegisterationViewModel viewModel)
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

            IdentityResult result = await AccountManager.Register(viewModel);
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
        public async Task<IActionResult> RegisterAsVendor([FromForm] VendorRegisterationViewModel viewModel)
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
            IdentityResult result = await AccountManager.RegisterAsVendor(viewModel);
            if (result.Succeeded) return Ok();
            else
            {
                List<IdentityError> Errors = new();
                foreach (IdentityError item in result.Errors)
                {
                    Errors.Add(item);
                }
                return BadRequest(Errors);
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] LoginViewModel viewModel)
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
            SignInResult SignInResult = await AccountManager.Login(viewModel);
            if (SignInResult.IsLockedOut) return new ObjectResult("Your Account is Under Review");
            else if (SignInResult.Succeeded)
            {
                User? User = await UserManager.FindByEmailAsync(viewModel.Email);
                string tokenString = await AccountManager.GenerateJSONWebToken(User!);
                IList<string> roles = await
                    UserManager.GetRolesAsync(User!);
                if (roles.Contains("vendor"))
                {
                    return Ok(new
                    {
                        token = tokenString,
                        role = roles,
                        profilePicture = User!.PicUrl,
                        name = User!.Name,
                        id = User!.Id,
                        vendorId = VendorManager.GetVendorIdByUserId(User.Id)
                    }) ;
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
        [HttpGet("Logout")]
        public async Task Logout() => await AccountManager.Logout();


    }
}

