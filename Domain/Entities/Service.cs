namespace Domain.Entities
{
    public class Service : BaseModel
    {
        public int VendorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<Reservation> Reservations { set; get; }
        public virtual ICollection<Review> Reviews { set; get; }
        public virtual ICollection<ServiceAttachment> ServiceAttachments { set; get; }
        public virtual PromoCode PromoCode { set; get; }
        public virtual Vendor Vendor { get; set; }
    }


}