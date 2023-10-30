using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.Data;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using ViewModels.UserViewModels;
using ViewModels.VendorViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Repository
{
    public class AccountManager
    {
        readonly UserManager<User> UserManager;
        readonly SignInManager<User> SignInManager;
        readonly IConfiguration Configuration;
        readonly VendorManager VendorManager;


        public AccountManager(
            UserManager<User> _userManager,
            SignInManager<User> _signInManager,
            VendorManager _vendorManager,
            IConfiguration _configuration
            )
        {
            UserManager = _userManager;
            SignInManager = _signInManager;
            Configuration = _configuration;
            VendorManager = _vendorManager;
        }

        public async Task<IdentityResult> Register(IUserRegisteration Data)
        {

            FileInfo fi = new(Data.Picture.FileName);
            string FileName = DateTime.Now.Ticks + fi.Extension;
            Data.PicUrl = FileName;
            var User = Data.ToUser();
            var result = await UserManager.CreateAsync(User, Data.Password);
            if (result.Succeeded)
            {
                result = await UserManager.AddToRoleAsync(User, "user");
                Helper.UploadMediaAsync(User.Id, "ProfilePicture", FileName, Data.Picture);
            }
            return result;
        }
        public async Task<IdentityResult> RegisterAsVendor(VendorRegisterationViewModel Data)
        {
            var Result = await Register(Data);

            if (Result.Succeeded)
            {
                Result = await UserManager.AddToRoleAsync
                    ((await UserManager.FindByEmailAsync(Data.Email))!,
                    "vendor");
                VendorManager.Add(Data.ToVendor((await UserManager.FindByEmailAsync(Data.Email))!));
                VendorManager.Save();
            }
            return Result;
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

        public async Task<string> GenerateJSONWebToken(string Email)
        {
            // Not Null Because this function is called after sigining in using sign in manager
            User User = UserManager.FindByEmailAsync(Email).Result!;
            var userRoles = await UserManager.GetRolesAsync(User);
            var roles = userRoles.Select(o => new Claim(ClaimTypes.Role, o));
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]!));
            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId,User.Id),
                new Claim(JwtRegisteredClaimNames.Name, User.Name),
                new Claim(JwtRegisteredClaimNames.UniqueName, User.UserName!),
                new Claim(JwtRegisteredClaimNames.Email, User.Email!),
            }
            .Union(roles);


            var token = new JwtSecurityToken(
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials,
              claims: claims);

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
