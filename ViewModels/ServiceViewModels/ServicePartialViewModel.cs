using ViewModels.ServiceAttachmentViewModels;

namespace ViewModels.ServiceViewModels
{
    public class ServicePartialViewModel
    {
        public required int Id { get; set; }
        public required string UserId { get;set; }
        public required int VendorId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required float Price { get; set; }
        public required int ReviewsCount { get; set; }
        public required int ReservationsCount { get; set; }
        public required IEnumerable<ServiceAttachmentViewModel> ServiceAttachments { set; get; }
    }
}

