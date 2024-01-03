using Application.DTOs.CustomerDTOs;
using Application.DTOs.PaginationDTOs;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.IRepositories
{
    public interface ICustomerRepository
    {
        public Task<PaginationDTO<CustomerDTO>> GetAll(int PageIndex, int PageSize);
        public Task<CustomerDTO?> GetByIdAsync(string id);
        public Task<int> Count();
        public Task<IdentityResult> UpdateAsync(UpdateCustomerDTO updateCustomerDTO, string userId);
    }
}
