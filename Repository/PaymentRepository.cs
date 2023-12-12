using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using System.Globalization;
using ViewModels.PaginationViewModels;
using ViewModels.PaymentViewModels;

namespace Repository
{
    public class PaymentRepository : BaseRepository<Payment>
    {
        private readonly EntitiesContext EntitiesContext;
        public PaymentRepository(EntitiesContext _dBContext, EntitiesContext entitiesContext) : base(_dBContext)
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
            return Get().Where(p => p.Reservation.Service.VendorId == VendorId && p.IsWithdrawn == false).Sum(v => v.Amount);
        }

        public void IsWithdrawn(int PaymentId)
        {
            var Data = Get(PaymentId)!;
            Data.IsWithdrawn = true;
            EntitiesContext.Update(Data);
        }

        public IDictionary<string, double> GetMonthlyPaymentTotal(int Year)
        {
            var monthlyTotals = new Dictionary<string, double>();

            for (int month = 1; month <= 12; month++)
            {
                var totalForMonth = EntitiesContext.Payments
                    .Where(p => p.DateTime.Month == month && p.DateTime.Year == Year)
                    .Sum(p => p.Amount) / 3;

                string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);

                monthlyTotals.Add(monthName, totalForMonth);
            }

            return monthlyTotals;
        }
    }
}
