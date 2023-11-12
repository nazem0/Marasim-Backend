using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using ViewModels.PaymentViewModel;

namespace Repository
{
    public class PaymentManager : MainManager<Payment>
    {
        private readonly EntitiesContext EntitiesContext;
        public PaymentManager(EntitiesContext _dBContext, EntitiesContext entitiesContext) : base(_dBContext)
        {
            EntitiesContext = entitiesContext;
        }
        public new IEnumerable<PaymentViewModel> Get()
        {
            return base.Get()
                .Include(p => p.Reservation.User)
                .Include(p => p.Reservation.Service.Vendor.User)
                .Select(p => p.ToPaymentViewModel());
        }
        public EntityEntry<Payment> Add(AddPaymentViewModel Data)
        {
            return EntitiesContext.Add(Data.ToPayment());
        }
    }
}
