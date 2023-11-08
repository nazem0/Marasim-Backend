using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.PaymentViewModel
{
    public static class PaymentExtensions
    {
        public static Payment ToPayment(this AddPaymentViewModel Data)
        {
            return new Payment
            {
                InstaPay = Data.InstaPay,
                ReservationId = Data.ReservationId,
            };
        }
    }
}
