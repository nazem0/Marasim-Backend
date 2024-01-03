namespace Domain.Entities
{
    public class Governorate : BaseModel
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }

        // Navigation property
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Vendor> Vendors { get; set; }
    }
}
