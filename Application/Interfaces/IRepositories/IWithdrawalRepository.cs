using Application.DTOs.PaginationDTOs;
using Application.DTOs.WithdrawalDTOs;
using System.Net;

namespace Application.Interfaces.IRepositories
{
    public interface IWithdrawalRepository
    {
        public PaginationDTO<WithdrawalDTO> GetWithdrawals(int PageIndex, int PageSize);
        public Task<HttpStatusCode> AddAsync(CreateWithdrawalDTO Data, int vendorId);
        public void Withdraw(int paymentId, int vendorId);
        public HttpStatusCode ConfirmWithdrawal(int WithdrawalId);
    }
}