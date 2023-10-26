using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ViewModels.UserViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;
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

        public async Task<IdentityResult> Register(RegisterationViewModel Data)
        {

            FileInfo fi = new (Data.Picture.FileName);
            string FileName = DateTime.Now.Ticks + fi.Extension;
            Data.PicURL = FileName;
            var User = Data.ToUser();
            var result = await UserManager.CreateAsync(User, Data.Password);
            if (result.Succeeded)
            {
                result = await UserManager.AddToRoleAsync(User, "User");
                Helper.UploadMediaAsync(User.Id, "ProfilePicture", FileName, Data.Picture);
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

        public async Task Logout()
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

        public async Task<IdentityResult> ChangePassword(ChangePasswordViewModel Data)
        {
            var user = await UserManager.FindByIdAsync(Data.ID);
            if (user != null)
            {
                return await UserManager.ChangePasswordAsync(user, Data.OldPassword, Data.NewPassword);
            }
            return IdentityResult.Failed(new IdentityError()
            {
                Description = "User Not Found"
            });
        }

        public async Task<IdentityResult> AssignRolesToUser(string UserId, List<string> roles)
        {
            var user = await UserManager.FindByIdAsync(UserId);
            if (user != null)
            {
                return await UserManager.AddToRolesAsync(user, roles);
            }
            return new IdentityResult();
        }
        
    }
}
