using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models;
using ViewModels.UserViewModels;

namespace Repository
{
    public class AccountManager
    {
        readonly UserManager<User> UserManager;
        readonly SignInManager<User> SignInManager;
        readonly IConfiguration Configuration;


        public AccountManager(
            UserManager<User> _userManager,
            SignInManager<User> _signInManager,
            IConfiguration _configuration
            )
        {
            UserManager = _userManager;
            SignInManager = _signInManager;
            Configuration = _configuration;
        }


        public async Task<IdentityResult> Register(RegisterationViewModel viewModel)
        {
            var model = viewModel.ToUser();
            var result = await UserManager.CreateAsync(model, viewModel.Password);
            if (result.Succeeded)
            {
                result = await UserManager.AddToRoleAsync(model, "User");
            }
            return result;
        }

        public async Task<SignInResult> Login(LoginViewModel viewModel)
        {
            var User = await UserManager.FindByEmailAsync(viewModel.Email);
            if (User != null)
            {
                return await SignInManager.PasswordSignInAsync(User,
                                  viewModel.Password, viewModel.RememberMe, false);
            }
            else
            {
                return SignInResult.Failed;
            }
        }

        public async void SignOut()
        {
            await SignInManager.SignOutAsync();
        }

        public string GenerateJSONWebToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //public async Task<IdentityResult> ChangePassword(UserChangePasswordViewModel viewModel)
        //{
        //    var user = await userManager.FindByIdAsync(viewModel.Id);
        //    if (user != null)
        //    {
        //        return await userManager.ChangePasswordAsync(user, viewModel.CurrentPassword, viewModel.NewPassword);
        //    }
        //    return IdentityResult.Failed(new IdentityError()
        //    {
        //        Description = "User Not Found"
        //    });
        //}
        //public async Task<string> GetForgotPasswordCode(string Email)
        //{
        //    var user = await userManager.FindByEmailAsync(Email);
        //    if (user != null)
        //    {
        //        var code = await userManager.GeneratePasswordResetTokenAsync(user);
        //        return code;
        //    }
        //    return string.Empty;
        //}
        //public async Task<IdentityResult> ForgotPassword(UserForgotPasswordViewModel viewModel)
        //{
        //    var user = await userManager.FindByEmailAsync(viewModel.Email);
        //    if (user != null)
        //    {
        //        return await userManager.ResetPasswordAsync(user, viewModel.Code, viewModel.NewPassword);
        //    }
        //    return IdentityResult.Failed(new IdentityError()
        //    {
        //        Description = "User Not Found"
        //    });
        //}
        //public async Task<IdentityResult> AssignRolesToUser(string UserId, List<string> roles)
        //{
        //    var user = await userManager.FindByIdAsync(UserId);
        //    if (user != null)
        //    {
        //        return await userManager.AddToRolesAsync(user, roles);
        //    }
        //    return new IdentityResult();
        //}

    }
}
