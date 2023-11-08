using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public EntityEntry<Payment> Add(AddPaymentViewModel Data)
        {
            return EntitiesContext.Add(Data.ToPayment());
        }
    }
}
