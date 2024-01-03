using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string PicUrl { get; set; }
        public bool Gender { get; set; }
        public string NationalId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime RegistrationDate { get; set; }
        public virtual ICollection<Follow> Follows { set; get; }
        public virtual ICollection<Review> Reviews { set; get; }
        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Invitation> WeddingInvitations { set; get; }
        public virtual ICollection<React> Reacts { set; get; }
        public virtual ICollection<Reservation> Reservations { set; get; }

    }
}



