using ViewModels.PromoCodeViewModels;
using ViewModels.ReservationViewModels;
using ViewModels.ServiceAttatchmentViewModels;

namespace ViewModels.ServiceViewModels
{
    public class ServiceViewModel
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public int VendorId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public float Price { get; set; }
        public int ReviewsCount { get; set; }
        public bool IsDeleted { get; set; }
        public virtual required IEnumerable<ServiceAttatchmentViewModel> ServiceAttachments { set; get; }
        public virtual PromoCodeViewModel? PromoCode { set; get; }
        public virtual required IEnumerable<VendorReservationViewModel> Reservations { set; get; }


    }
}
