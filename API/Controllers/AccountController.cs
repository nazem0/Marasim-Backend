using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System.Text;
using ViewModels.User;

namespace API.Controllers
{
	public class AccountController : ControllerBase
    {
        AccountManager accManger;
        public AccountController(AccountManager _accManger)
        {
            this.accManger = _accManger;
        }

        public async Task<IActionResult> SignIn([FromBody] LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await accManger.Login(viewModel);
                if (result.Succeeded)
                {
                    return Ok();
                }
                else if (result.IsLockedOut)
                {
                    return new ObjectResult("Your Account is Under Review");
                }
                else
                {
                    return new ObjectResult("User name or Password");
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


        public async Task<IActionResult> SignUp([FromBody] RegisterationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await accManger.Register(viewModel);
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

