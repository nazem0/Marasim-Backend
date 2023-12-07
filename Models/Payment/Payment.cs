using System;

namespace Models
{
    public class Payment : BaseModel
    {
        public string InstaPay;
        public float Amount;
        public DateTime DateTime;
        public int ReservationId;
        public bool IsWithdrawn;
        public int? WithdrawalId;
        public virtual Reservation Reservation { get; set; }
        public virtual Withdrawal Withdrawal { get; set; }
    }
}
