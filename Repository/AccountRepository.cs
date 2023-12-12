using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ViewModels.UserViewModels;
using ViewModels.VendorViewModels;
namespace Repository
{
    public class AccountRepository
    {
        readonly UserManager<User> UserManager;
        readonly SignInManager<User> SignInManager;
        readonly IConfiguration Configuration;
        readonly VendorRepository VendorManager;


        public AccountRepository(
            UserManager<User> _userManager,
            SignInManager<User> _signInManager,
            VendorRepository _vendorManager,
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
        public async Task<bool> RegisterAsVendor(VendorRegistrationViewModel Data)
        {
            FileInfo fi = new(Data.Picture.FileName);
            string FileName = DateTime.Now.Ticks + fi.Extension;
            Data.PicUrl = FileName;
            var User = Data.ToUser();

            var result = await UserManager.CreateAsync(User, Data.Password);
            if (!result.Succeeded) return false;

            await UserManager.AddToRoleAsync(User, "vendor");
            Helper.UploadMediaAsync(User.Id, "ProfilePicture", FileName, Data.Picture);

            EntityEntry VendorAddition = VendorManager.Add(Data.ToVendor((await UserManager.FindByEmailAsync(Data.Email))!));
            if (VendorAddition.State.ToString() != "Added")
            {
                await UserManager.DeleteAsync(User);
                return false;
            }
            else
            {
                VendorManager.Save();
                return true;
            }


        }
        public async Task<SignInResult> Login(LoginViewModel viewModel)
        {
            var User = await UserManager.FindByEmailAsync(viewModel.Email);
            if (User != null)
                return await SignInManager.PasswordSignInAsync
                    (User, viewModel.Password, viewModel.RememberMe, viewModel.RememberMe);
            else
                return SignInResult.Failed;
        }

        public async Task Logout()
        {
            await SignInManager.SignOutAsync();
        }

        public async Task<string> GenerateJSONWebToken(User User)
        {
            // Not Null Because this function is called after signing in using sign in manager
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
              expires: DateTime.Now.AddDays(30),
              signingCredentials: credentials,
              claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<IdentityResult> ChangePassword(string UserId, ChangePasswordViewModel Data)
        {
            var User = await UserManager.FindByIdAsync(UserId);
            if (User != null)
            {
                return await UserManager.ChangePasswordAsync(User, Data.OldPassword, Data.NewPassword);
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
