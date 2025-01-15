using Application.DTOs.PaginationDTOs;
using Application.DTOs.ReservationDTOs;
using Application.ExtensionMethods;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Net;

namespace Persistence.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly IPromoCodeRepository _promoCodeRepository;
        private readonly DbSet<Reservation> _reservations;
        private readonly DbSet<Service> _services;
        private readonly IUnitOfWork _unitOfWork;
        public ReservationRepository(AppDbContext entitiesContext, IPromoCodeRepository promoCodeManager, IUnitOfWork unitOfWork)
        {
            _reservations = entitiesContext.Reservations;
            _promoCodeRepository = promoCodeManager;
            _services = entitiesContext.Services;
            _unitOfWork = unitOfWork;
        }
        public HttpStatusCode Add(CreateReservationDTO Data)
        {
            Reservation Reservation = Data.ToEntity();
            Service? ReservedService = _services.Find(Data.ServiceId);
            if (ReservedService is null) return HttpStatusCode.NotFound;

            Reservation.Price = ReservedService.Price;
            if (Data.PromoCode is not null)
            {
                Reservation.Price -= _promoCodeRepository.UsePromoCode(Data.PromoCode, Data.ServiceId);
            }

            _reservations.Add(Reservation);
            return _unitOfWork.SaveChanges();
        }
        public HttpStatusCode ChangeStatusByVendor(int ReservationId, char Status, int vendorId)
        {
            Reservation? Reservation = _reservations.Where(r => r.Id == ReservationId && r.Service.VendorId == vendorId).FirstOrDefault();
            if (Reservation == null) return HttpStatusCode.NotFound;
            Reservation.Status = Status;
            return _unitOfWork.SaveChanges();
        }
        public HttpStatusCode ChangeStatusByCustomer(int ReservationId, char Status, string customerId)
        {
            Reservation? Reservation = _reservations.Where(r => r.Id == ReservationId && r.UserId == customerId).FirstOrDefault();
            if (Reservation == null) return HttpStatusCode.NotFound;
            Reservation.Status = Status;
            return _unitOfWork.SaveChanges();
        }
        public HttpStatusCode Confirm(int ReservationId)
        {
            Reservation? Reservation = _reservations.Find(ReservationId);
            if (Reservation == null) return HttpStatusCode.NotFound;
            Reservation.Status = 'c';
            _reservations.Update(Reservation);
            return _unitOfWork.SaveChanges();
        }

        // User Reservations
        public PaginationDTO<CustomerReservationDTO> GetUserReservations(string UserId, int PageIndex, int PageSize)
        {
            return _reservations
                .Where(r => r.UserId == UserId)
                .Select(r => r.ToCustomerReservationDTO())
                .ToPaginationDTO(PageIndex, PageSize);
        }
        public PaginationDTO<CustomerReservationDTO> GetUserReservationsByIdAndStatus(string UserId, char Status, int PageIndex, int PageSize)
        {
            return _reservations
                .Where(r => r.UserId == UserId && r.Status == Status)
                .Select(r => r.ToCustomerReservationDTO())
                .ToPaginationDTO(PageIndex, PageSize);
        }

        // Vendor Reservations
        public PaginationDTO<VendorReservationDTO> GetVendorReservations(int VendorId, int PageIndex, int PageSize)
        {
            return _reservations
                .Where(r => r.Service.VendorId == VendorId)
                .Select(r => r.ToVendorReservationDTO())
                .ToPaginationDTO(PageIndex, PageSize);
        }
        public PaginationDTO<VendorReservationDTO> GetVendorReservationsByIdAndStatus(int VendorId, char Status, int PageIndex, int PageSize)
        {
            return _reservations
                .Where(r => r.Service.VendorId == VendorId && r.Status == Status)
                .Select(r => r.ToVendorReservationDTO())
                .ToPaginationDTO(PageIndex, PageSize);

        }
        public CheckoutReservationDTO? CheckoutReservationById(string UserId, int ReservationId)
        {
            return _reservations
                .Where(r => r.UserId == UserId && r.Id == ReservationId && r.Status == 'a')
                .Select(r => r.ToCheckoutReservationDTO())
                .FirstOrDefault();
        }

        // Stats
        public Dictionary<string, int> GetReservationTotalDoneOrders(int vendorId, int year)
        {
            var reservations = _reservations
                .Where(r => r.Service.VendorId == vendorId && r.Status == 'd' && r.DateTime.Year == year);

            var totalOrders = new Dictionary<string, int>();
            for (int i = 0; i < CultureInfo.CurrentCulture.DateTimeFormat.MonthNames.Length; i++)
            {
                var month = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[i];
                totalOrders.Add(month, reservations.Where(r => r.DateTime.Month == i + 1).Count());
            }
            return totalOrders;
        }
        public Dictionary<string, float> GetReservationTotalSales(int vendorId, int year)

        {
            var reservations = _reservations
                .Where(r => r.Service.VendorId == vendorId && r.Status == 'd' && r.DateTime.Year == year);

            var totalSales = new Dictionary<string, float>();
            for (int i = 0; i < CultureInfo.CurrentCulture.DateTimeFormat.MonthNames.Length; i++)
            {
                var month = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[i];
                totalSales.Add(month, reservations.Where(r => r.DateTime.Month == i + 1).Sum(r => r.Price));
            }
            return totalSales;
        }
    }
}