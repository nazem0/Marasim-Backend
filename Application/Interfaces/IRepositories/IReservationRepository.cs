using Application.DTOs.PaginationDTOs;
using Application.DTOs.ReservationDTOs;
using System.Net;

namespace Application.Interfaces.IRepositories
{
    public interface IReservationRepository
    {
        public HttpStatusCode Add(CreateReservationDTO Data);
        public HttpStatusCode ChangeStatusByCustomer(int ReservationId, char Status, string customerId);
        public HttpStatusCode ChangeStatusByVendor(int ReservationId, char Status, int vendorId);
        public HttpStatusCode Confirm(int ReservationId);

        // User Reservations
        public PaginationDTO<CustomerReservationDTO> GetUserReservations(string UserId, int PageIndex, int PageSize);
        public PaginationDTO<CustomerReservationDTO> GetUserReservationsByIdAndStatus(string UserId, char Status, int PageIndex, int PageSize);

        // Vendor Reservations
        public PaginationDTO<VendorReservationDTO> GetVendorReservations(int VendorId, int PageIndex, int PageSize);
        public PaginationDTO<VendorReservationDTO> GetVendorReservationsByIdAndStatus(int VendorId, char Status, int PageIndex, int PageSize);
        public CheckoutReservationDTO? CheckoutReservationById(string UserId, int ReservationId);

        // Stats
        public Dictionary<string, int> GetReservationTotalDoneOrders(int vendorId, int year);
        public Dictionary<string, float> GetReservationTotalSales(int vendorId, int year);
    }
}