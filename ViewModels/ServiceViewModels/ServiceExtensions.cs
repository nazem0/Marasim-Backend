using Models;

namespace ViewModels.ServiceViewModels
{
    public static class ServiceExtensions
    {
        public static Service ToModel(this CreateServiceViewModel Data, int VendorID)
        {
            return new Service()
            {
                Title = Data.Title,
                Description = Data.Description,
                Price = Data.Price,
                VendorID = VendorID
            };
        }
        public static ServiceViewModel ToServiceViewModel(this Service Data,string UserId)
        {
            return new ServiceViewModel
            {
                UserId = UserId,
                ServiceAttachments = Data.ServiceAttachments,
                Description = Data.Description,
                PromoCode = Data.PromoCode,
                Title = Data.Title,
                IsDeleted = Data.IsDeleted,
                Price = Data.Price,
                VendorID = Data.VendorID,
                ReviewsCount = Data.Reviews.Count(),
                BookingDetails = Data.BookingDetails,
            };
        }
    }
}