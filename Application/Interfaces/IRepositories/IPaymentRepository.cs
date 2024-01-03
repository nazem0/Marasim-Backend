using Application.DTOs.PaginationDTOs;
using Application.DTOs.PaymentDTOs;
using System.Net;

namespace Application.Interfaces.IRepositories
{
    public interface IPaymentRepository
    {
        public IEnumerable<PaymentDTO> GetPayments();
        public IEnumerable<PaymentDTO> GetUnconfirmed();
        public IEnumerable<PaymentDTO> GetConfirmed();
        public HttpStatusCode Add(CreatePaymentDTO createPaymentDTO);
        public PaginationDTO<VendorPaymentDTO> GetVendorsPayment(int vendorId, int pageIndex, int pageSize = 2);
        public double VendorBalance(int vendorId);
        public IDictionary<string, double> GetMonthlyPaymentTotal(int year);
        public int Count();
    }
}
