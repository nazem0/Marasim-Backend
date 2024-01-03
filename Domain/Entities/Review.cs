namespace Domain.Entities
{
    public class Review : BaseModel
    {
        public string UserId { get; set; }
        public int ServiceId { get; set; }
        public int ReservationId { get; set; }
        public int Rate { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
        public virtual User User { get; set; }
        public virtual Service Service { get; set; }
        public virtual Reservation Reservation { get; set; }
    }
}
