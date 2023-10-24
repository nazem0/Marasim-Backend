using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using ViewModels.User;

namespace Repository
{
    public class AccountManager
    {
        UserManager<User> UserManager;
        SignInManager<User> SignInManager;

        public AccountManager(UserManager<User> _userManager,SignInManager<User> _signInManager)
        {
            UserManager = _userManager;
            SignInManager = _signInManager;
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
