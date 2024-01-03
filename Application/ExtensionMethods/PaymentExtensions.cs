using Application.DTOs.PaymentDTOs;
using Domain.Entities;

namespace Application.ExtensionMethods
{
    public static class PaymentExtensions
    {
        public static Payment ToEntity(this CreatePaymentDTO createPaymentDTO)
        {
            return new Payment
            {
                InstaPay = createPaymentDTO.InstaPay,
                Amount = createPaymentDTO.Amount,
                ReservationId = createPaymentDTO.ReservationId,
                DateTime = DateTime.Now,
            };
        }

        public static PaymentDTO ToPaymentDTO(this Payment payment)
        {
            return new PaymentDTO
            {
                InstaPay = payment.InstaPay,
                Amount = payment.Amount,
                DateTime = payment.DateTime,
                Reservation = payment.Reservation.ToAdminReservationDTO()
            };
        }
        public static VendorPaymentDTO ToVendorPaymentDTO(this Payment payment)
        {
            return new VendorPaymentDTO
            {
                DateTime = payment.DateTime,
                Amount = payment.Amount,
                ServiceTitle = payment.Reservation.Service.Title,
                User = payment.Reservation.User.ToCustomerMinInfoDTO(),
            };
        }
    }
}
