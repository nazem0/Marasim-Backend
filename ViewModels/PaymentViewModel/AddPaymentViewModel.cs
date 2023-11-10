using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.PaymentViewModel
{
    public class AddPaymentViewModel
    {
        [Required]
        public required string InstaPay { get; set; }
        [Required]
        public int ReservationId { get; set; }
    }
}
