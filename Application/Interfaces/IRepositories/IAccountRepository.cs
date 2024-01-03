using Application.DTOs.AccountDTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.IRepositories
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> Register(CustomerRegisterDTO customerRegisterDTO);
        public Task<bool> RegisterAsVendor(VendorRegisterDTO vendorRegisterDTO);
        public Task<SignInResult> Login(LoginDTO loginDTO);
        public Task Logout();
        public Task<string> GenerateJSONWebToken(User user);
        public Task<IdentityResult> ChangePassword(string userId, ChangePasswordDTO changePasswordDTO);
        //Dont Remember if this is actually used ?
        //public Task<IdentityResult> AssignRolesToUser(string userId, List<string> roles);
    }
}
