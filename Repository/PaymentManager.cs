using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using ViewModels.PaginationViewModels;
using ViewModels.PaymentViewModels;

namespace Repository
{
    public class PaymentManager : MainManager<Payment>
    {
        private readonly EntitiesContext EntitiesContext;
        public PaymentManager(EntitiesContext _dBContext, EntitiesContext entitiesContext) : base(_dBContext)
        {
            EntitiesContext = entitiesContext;
        }
        public IEnumerable<PaymentViewModel> GetPayments()
        {
            return
                EntitiesContext.Payments
                .Select(p => p.ToPaymentViewModel());
        }
        public IEnumerable<PaymentViewModel> GetUnconfirmed()
        {
            return
                EntitiesContext.Payments
                .Where(p => p.Reservation.Status == 'f')
                .Select(p => p.ToPaymentViewModel());
        }
        public IEnumerable<PaymentViewModel> GetConfirmed()
        {
            return
                EntitiesContext.Payments
                .Where(p => p.Reservation.Status != 'f')
                .Select(p => p.ToPaymentViewModel());
        }
        public EntityEntry<Payment> Add(AddPaymentViewModel Data)
        {
            return EntitiesContext.Add(Data.ToPayment());
        }
        public PaginationViewModel<VendorPaymentViewModel> GetVendorsPayment(int VendorId, int PageIndex, int PageSize = 2)
        {
            PaginationDTO<VendorPaymentViewModel> PaginationDTO = new()
            {
                PageIndex = PageIndex,
                PageSize = PageSize
            };
            return Get()
                .Where(p => p.Reservation.Service.VendorId == VendorId)
                .OrderByDescending(p => p.DateTime)
                .Select(p => p.ToVendorPaymentViewModel())
                .ToPaginationViewModel(PaginationDTO);
        }
        public double VendorBalance(int VendorId)
        {

            return Get().Where(v => v.Reservation.Service.VendorId == VendorId).Sum(v => v.Reservation.Price * 0.3);
        }
    }
}
