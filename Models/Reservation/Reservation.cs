namespace Models
{
    public class Reservation : BaseModel
    {
        public string UserId { get; set; }
        public int ServiceId { get; set; }
        public float Price { get; set; }
        public char Status { get; set; }
        public virtual User User { get; set; }
        public virtual Service Service { get; set; }
    }
}
