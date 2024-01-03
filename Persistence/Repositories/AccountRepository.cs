using Application.DTOs.AccountDTOs;
using Application.ExtensionMethods;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace Persistence.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        readonly SignInManager<User> _signInManager;
        readonly IConfiguration _configuration;
        readonly IUnitOfWork _unitOfWork;
        readonly EntitiesContext _entitiesContext;
        readonly UserManager<User> _userManager;
        public AccountRepository
            (SignInManager<User> signInManager,
            IConfiguration configuration,
            IUnitOfWork unitOfWork,
            EntitiesContext entitiesContext,
            UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _entitiesContext = entitiesContext;
            _userManager = userManager;
        }

        public async Task<IdentityResult> Register(CustomerRegisterDTO customerRegisterDTO)
        {

            FileInfo fi = new(customerRegisterDTO.Picture.FileName);
            string fileName = DateTime.Now.Ticks + fi.Extension;
            customerRegisterDTO.PicUrl = fileName;
            User user = customerRegisterDTO.ToEntity();
            var result = await _userManager.CreateAsync(user, customerRegisterDTO.Password);

            if (!result.Succeeded) return result;

            result = await _userManager.AddToRoleAsync(user, "user");
            Helper.UploadMediaAsync(user.Id, "ProfilePicture", fileName, customerRegisterDTO.Picture);

            return result;
        }
        public async Task<bool> RegisterAsVendor(VendorRegisterDTO vendorRegisterDTO)
        {
            FileInfo fi = new(vendorRegisterDTO.Picture.FileName);
            string FileName = DateTime.Now.Ticks + fi.Extension;
            vendorRegisterDTO.PicUrl = FileName;
            User user = vendorRegisterDTO.ToEntity();

            var result = await _userManager.CreateAsync(user, vendorRegisterDTO.Password);
            if (!result.Succeeded) return false;

            await _userManager.AddToRoleAsync(user, "vendor");
            Helper.UploadMediaAsync(user.Id, "ProfilePicture", FileName, vendorRegisterDTO.Picture);

            _entitiesContext.Vendors.Add(vendorRegisterDTO.ToEntity(user));
            return _unitOfWork.SaveChanges() > 0;
        }

        public async Task<SignInResult> Login(LoginDTO loginDTO)
        {
            var User = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (User is null) return SignInResult.Failed;

            return await _signInManager.PasswordSignInAsync
                (User, loginDTO.Password, loginDTO.RememberMe, loginDTO.RememberMe);
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<string> GenerateJSONWebToken(User user)
        {
            // Not Null Because this function is called after signing in using sign in manager
            var userRoles = await _userManager.GetRolesAsync(user);
            var roles = userRoles.Select(o => new Claim(ClaimTypes.Role, o));
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId,user.Id),
                new Claim(JwtRegisteredClaimNames.Name, user.Name),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            }
            .Union(roles);

            var token = new JwtSecurityToken(
              expires: DateTime.Now.AddDays(30),
              signingCredentials: credentials,
              claims: claims);
            string generatedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return generatedToken;
        }

        public async Task<IdentityResult> ChangePassword(string userId, ChangePasswordDTO changePasswordDTO)
        {
            var User = await _userManager.FindByIdAsync(userId);
            if (User is null)
                return IdentityResult.Failed(new IdentityError() { Description = "User Not Found" });
            return await _userManager.ChangePasswordAsync(User, changePasswordDTO.OldPassword, changePasswordDTO.NewPassword);
        }
        //Dont Remember if this is actually used ?
        //public async Task<IdentityResult> AssignRolesToUser(string userId, List<string> roles)
        //{
        //    var user = await FindByIdAsync(userId);
        //    if (user is null) return IdentityResult.Failed();
        //    return await AddToRolesAsync(user, roles);
        //}

    }
}
