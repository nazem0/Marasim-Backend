using Application.DTOs.PaginationDTOs;
using Application.DTOs.PaymentDTOs;
using Application.ExtensionMethods;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Net;

namespace Persistence.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly DbSet<Payment> _payments;
        private readonly DbSet<Reservation> _reservations;
        private readonly IUnitOfWork _unitOfWork;
        public PaymentRepository(EntitiesContext entitiesContext, IUnitOfWork unitOfWork)
        {
            _payments = entitiesContext.Payments;
            _reservations = entitiesContext.Reservations;
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<PaymentDTO> GetPayments()
        {
            return
                _payments
                .Select(p => p.ToPaymentDTO());
        }
        public IEnumerable<PaymentDTO> GetUnconfirmed()
        {
            return
                _payments
                .Where(p => p.Reservation.Status == 'f')
                .Select(p => p.ToPaymentDTO());
        }
        public IEnumerable<PaymentDTO> GetConfirmed()
        {
            return
                _payments
                .Where(p => p.Reservation.Status != 'f')
                .Select(p => p.ToPaymentDTO());
        }
        public HttpStatusCode Add(CreatePaymentDTO createPaymentDTO)
        {
            Reservation? Reservation =
                _reservations
                .Where(
                r => r.Id == createPaymentDTO.ReservationId &&
                r.Status == 'p')
                .FirstOrDefault();

            if (Reservation == null) return HttpStatusCode.NotFound;

            _payments.Add(createPaymentDTO.ToEntity());
            Reservation.Status = 'f';
            _reservations.Update(Reservation);

            return _unitOfWork.SaveChanges();
        }
        public PaginationDTO<VendorPaymentDTO> GetVendorsPayment(int vendorId, int pageIndex, int pageSize = 2)
        {
            return _payments
                .Where(p => p.Reservation.Service.VendorId == vendorId)
                .OrderByDescending(p => p.DateTime)
                .Select(p => p.ToVendorPaymentDTO())
                .ToPaginationDTO(pageIndex, pageSize);
        }
        public double VendorBalance(int vendorId)
        {
            return
                _payments
                .Where(p => p.Reservation.Service.VendorId == vendorId && p.IsWithdrawn == false)
                .Sum(v => v.Amount);
        }


        public IDictionary<string, double> GetMonthlyPaymentTotal(int year)
        {
            var monthlyTotals = new Dictionary<string, double>();

            for (int month = 1; month <= 12; month++)
            {
                var totalForMonth = _payments
                    .Where(p => p.DateTime.Month == month && p.DateTime.Year == year)
                    .Sum(p => p.Amount) / 3;

                string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);

                monthlyTotals.Add(monthName, totalForMonth);
            }

            return monthlyTotals;
        }

        public int Count()
        {
            return _payments.Count();
        }
    }
}
