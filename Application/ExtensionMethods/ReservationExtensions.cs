using Application.DTOs.ReservationDTOs;
using Domain.Entities;

namespace Application.ExtensionMethods
{
    public static class ReservationExtensions
    {
        public static Reservation ToEntity(this CreateReservationDTO createReservationDTO)
        {
            return new Reservation
            {
                UserId = createReservationDTO.UserId,
                ServiceId = createReservationDTO.ServiceId,
                DateTime = createReservationDTO.DateTime,
                CityId = createReservationDTO.CityId,
                GovernorateId = createReservationDTO.GovId,
                Street = createReservationDTO.Street ?? "",
                District = createReservationDTO.District
            };
        }

        public static ChangeReservationStatusDTO ToChangeReservationStatusDTO(this Reservation reservation)
        {
            return new ChangeReservationStatusDTO
            {
                Id = reservation.Id,
                VendorId = reservation.Service.VendorId
            };
        }
        public static CustomerReservationDTO ToCustomerReservationDTO(this Reservation reservation)
        {
            return new CustomerReservationDTO
            {
                Id = reservation.Id,
                ServiceId = reservation.ServiceId,
                DateTime = reservation.DateTime,
                Price = reservation.Price,
                Status = reservation.Status,
                City = reservation.City.NameAr,
                Gov = reservation.Governorate.NameAr,
                Street = reservation.Street,
                District = reservation.District,
                Vendor = reservation.Service.Vendor.ToVendorMinInfoDTO(),
                Review = reservation.Review.ToReviewDTO(),
                IsDeleted = reservation.IsDeleted
            };
        }
        public static VendorReservationDTO ToVendorReservationDTO(this Reservation reservation)
        {
            return new VendorReservationDTO
            {
                Id = reservation.Id,
                ServiceId = reservation.ServiceId,
                DateTime = reservation.DateTime,
                Price = reservation.Price,
                Status = reservation.Status,
                City = reservation.City.NameAr,
                Gov = reservation.Governorate.NameAr,
                Street = reservation.Street,
                District = reservation.District,
                User = reservation.User.ToCustomerDTO(),
                Service = reservation.Service.ToVendorServiceDTO()
            };
        }
        public static CheckoutReservationDTO ToCheckoutReservationDTO(this Reservation reservation)
        {
            return new CheckoutReservationDTO
            {
                City = reservation.City.NameAr,
                Gov = reservation.Governorate.NameAr,
                Street = reservation.Street,
                District = reservation.District,
                ServiceName = reservation.Service.Title,
                DateTime = reservation.DateTime,
                Price = (float)Math.Ceiling(reservation.Price * 0.3),
                Vendor = reservation.Service.Vendor.ToVendorMinInfoDTO()
            };
        }
        public static AdminReservationDTO ToAdminReservationDTO(this Reservation reservation)
        {
            return new AdminReservationDTO
            {
                Id = reservation.Id,
                ServiceId = reservation.ServiceId,
                DateTime = reservation.DateTime,
                Price = reservation.Price,
                Status = reservation.Status,
                City = reservation.City.NameAr,
                Gov = reservation.Governorate.NameAr,
                Street = reservation.Street,
                District = reservation.District,
                User = reservation.User.ToCustomerMinInfoDTO(),
                Vendor = reservation.Service.Vendor.ToVendorMinInfoDTO()
            };
        }
    }
}
