using ViewModels.PromoCodeViewModels;
using ViewModels.ReservationViewModels;
using ViewModels.ServiceAttatchmentViewModels;

namespace ViewModels.ServiceViewModels
{
    public class ShowAllServicesViewModel
    {
        public required int Id { get; set; }
        public required int VendorId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required float Price { get; set; }
        public required int ReviewsCount { get; set; }
        public required bool IsDeleted { get; set; }
        public virtual required IEnumerable<ServiceAttatchmentViewModel> ServiceAttachments { set; get; }
        public virtual PromoCodeViewModel? PromoCode { set; get; } = null;
        public virtual IEnumerable<AdminReservationViewModel>? Reservations { set; get; } = new List<AdminReservationViewModel>();
    }
}
