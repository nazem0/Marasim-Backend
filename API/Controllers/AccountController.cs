using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System.Text;
using ViewModels.UserViewModels;

namespace API.Controllers
{
    public class AccountController : ControllerBase
    {
        readonly AccountManager AccountManager;
        public AccountController(AccountManager _accManger)
        {
            AccountManager = _accManger;
        }

        public async Task<IActionResult> SignIn([FromBody] LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await AccountManager.Login(viewModel);

                if (user.Succeeded)
                {
                    string tokenString = AccountManager.GenerateJSONWebToken();
                    return Ok(new { token = tokenString });
                }
                else if (user.IsLockedOut)
                {
                    return new ObjectResult("Your Account is Under Review");
                }
                else
                {
                    return new ObjectResult("User name or Password is Wrong");
                }

            }
            else
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
        }


        public async Task<IActionResult> SignUp([FromBody] RegisterationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await AccountManager.Register(viewModel);
                if (result.Succeeded)
                {
                    return Ok();
                }
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
    }
}

