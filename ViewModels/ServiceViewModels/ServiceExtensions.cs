using Models;
using ViewModels.ServiceAttatchmentViewModels;
using ViewModels.UserViewModels;

namespace ViewModels.ServiceViewModels
{
    public static class ServiceExtensions
    {
        public static Service ToModel(this CreateServiceViewModel Data, int VendorId)
        {
            return new Service()
            {
                Title = Data.Title,
                Description = Data.Description,
                Price = Data.Price,
                VendorId = VendorId
            };
        }
        public static ServiceViewModel ToServiceViewModel(this Service Data, string UserId)
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
                VendorId = Data.VendorId,
                Id = Data.Id,
                ReviewsCount = Data.Reviews.Count,
                Reservations = Data.Reservations,
            };
        }


        public static ServicePartialViewModel ToServicePartialViewModel(this Service Data)
        {
            return new ServicePartialViewModel
            {
                ServiceAttachments = Data.ServiceAttachments.Select(sa => sa.ToViewModel()),
                Description = Data.Description,
                Title = Data.Title,
                //IsDeleted = Data.IsDeleted,
                Price = Data.Price,
                VendorId = Data.VendorId,
                Id = Data.Id,
                //ReviewsCount = Data.Reviews.Count,
                //ReservationsCount = Data.Reservations.Count,
            };
        }


        public static ServiceMinInfoViewModel ToServiceMinInfoViewModel(this Service Service)
        {
            return new ServiceMinInfoViewModel
            {
                VendorId = Service.VendorId,
                Title =Service.Title
            };
        }
    }
}