using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using System.Text;
using ViewModels.UserViewModels;
using ViewModels.VendorViewModels;

namespace API.Controllers
{
    public class AccountController : ControllerBase
    {
        readonly AccountManager AccountManager;
        public AccountController(AccountManager _accManger)
        {
            AccountManager = _accManger;
        }
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
            if (result.Succeeded) return Ok("Your Account Has Been Registered Successfully");
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
        public async Task<IActionResult> RegisterAsVendor([FromForm] VendorRegisterationViewModel viewModel)
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
            IdentityResult result = await AccountManager.RegisterAsVendor(viewModel);
            if (result.Succeeded) return Ok("Your Account Has Been Registered Successfully");
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
            var user = await AccountManager.Login(viewModel);
            if (user.Succeeded)
            {
                
                string tokenString = await AccountManager.GenerateJSONWebToken(viewModel.Email);
                return Ok(new { token = tokenString });
            }
            else if (user.IsLockedOut) return new ObjectResult("Your Account is Under Review");
            else return new ObjectResult("User name or Password is Wrong");


        }
        public async Task Logout() => await AccountManager.Logout();


    }
}

