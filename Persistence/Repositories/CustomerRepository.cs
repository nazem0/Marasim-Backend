using Application.DTOs.CustomerDTOs;
using Application.DTOs.PaginationDTOs;
using Application.ExtensionMethods;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly UserManager<User> _userManager;
        public CustomerRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }


        public async Task<PaginationDTO<CustomerDTO>> GetAll(int PageIndex, int PageSize)
        {
            IList<User> usersInRole = await _userManager.GetUsersInRoleAsync("user");
            return usersInRole
                .OrderByDescending(u => u.RegistrationDate)
                .Select(u => u.ToCustomerDTO())
                .ToPaginationDTO(PageIndex, PageSize);
        }

        public async Task<int> Count()
        {
            IList<User> usersInRole = await _userManager.GetUsersInRoleAsync("user");
            return usersInRole.Count;
        }

        public async Task<CustomerDTO?> GetByIdAsync(string id)
        {
            User? user = await _userManager.FindByIdAsync(id);
            return user?.ToCustomerDTO();
        }

        public async Task<IdentityResult> UpdateAsync(UpdateCustomerDTO updateCustomerDTO, string userId)
        {
            User? user = await _userManager.FindByIdAsync(userId);
            if (user is null) return IdentityResult.Failed(new IdentityError { Description = "User Does not Exist" });
            if (updateCustomerDTO.Picture is not null)
            {
                Helper.DeleteMediaAsync(user.Id, "ProfilePicture", user.PicUrl);
                FileInfo fi = new(updateCustomerDTO.Picture.FileName);
                string FileName = DateTime.Now.Ticks + fi.Extension;
                user.Name = updateCustomerDTO.Name ?? user.Name;
                Helper.UploadMediaAsync(user.Id, "ProfilePicture", FileName, updateCustomerDTO.Picture);
                updateCustomerDTO.PicURL = FileName;
                user.PicUrl = updateCustomerDTO.PicURL ?? user.PicUrl;
            }

            user.Name = updateCustomerDTO.Name ?? user.Name;
            user.PhoneNumber = updateCustomerDTO.PhoneNumber ?? user.PhoneNumber;
            var result = await _userManager.UpdateAsync(user);
            return result;
        }
    }
}
