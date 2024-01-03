using Application.DTOs.PaginationDTOs;
using Application.DTOs.WithdrawalDTOs;
using Application.ExtensionMethods;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Persistence.Repositories
{
    public class WithdrawalRepository : IWithdrawalRepository
    {
        private readonly EntitiesContext _entitiesContext;
        private readonly DbSet<Withdrawal> _withdrawals;
        private readonly DbSet<Payment> _payments;
        private readonly IUnitOfWork _unitOfWork;
        public WithdrawalRepository(EntitiesContext entitiesContext, IUnitOfWork unitOfWork)
        {
            _entitiesContext = entitiesContext;
            _withdrawals = entitiesContext.Withdrawals;
            _payments = entitiesContext.Payments;
            _unitOfWork = unitOfWork;
        }

        public PaginationDTO<WithdrawalDTO> GetWithdrawals(int PageIndex, int PageSize)
        {
            return _withdrawals
                .Select(w => w.ToWithdrawalDTO())
                .ToPaginationDTO(PageIndex, PageSize);
        }

        public async Task<HttpStatusCode> AddAsync(CreateWithdrawalDTO Data, int vendorId)
        {
            ICollection<Payment> Payments = await _payments.Where(p => p.Reservation.Service.VendorId == vendorId && p.IsWithdrawn == false).ToListAsync();
            _entitiesContext.Add(Data.ToEntity(vendorId, Payments));
            foreach (var Payment in Payments)
            {
                Withdraw(Payment.Id, vendorId);
            }
            _unitOfWork.SaveChanges();
            return _unitOfWork.SaveChanges();

        }
        public void Withdraw(int paymentId, int vendorId)
        {
            Payment? payment = _payments
                .Where
                (
                p => p.Id == paymentId &&
                p.Reservation.Service.VendorId == vendorId &&
                p.IsWithdrawn == false
                )
                .FirstOrDefault();
            if (payment is null) return;
            payment.IsWithdrawn = true;
            _payments.Update(payment);
        }
        public HttpStatusCode ConfirmWithdrawal(int WithdrawalId)
        {
            Withdrawal? withdrawal = _withdrawals.Find(WithdrawalId);
            if (withdrawal is null) return HttpStatusCode.NotFound;
            withdrawal.IsConfirmed = true;
            _entitiesContext.Update(withdrawal);
            return _unitOfWork.SaveChanges();

        }
    }
}