using Application.DTOs.ServiceDTOs;
using Domain.Entities;

namespace Application.ExtensionMethods
{
    public static class ServiceExtensions
    {
        public static Service ToEntity(this CreateServiceDTO createServiceDTO, int loggedInVendorId)
        {
            return new Service()
            {
                Title = createServiceDTO.Title,
                Description = createServiceDTO.Description,
                Price = createServiceDTO.Price,
                VendorId = loggedInVendorId
            };
        }

        public static VendorServiceDTO ToVendorServiceDTO(this Service service)
        {
            return new VendorServiceDTO
            {
                UserId = service.Vendor.UserId,
                ServiceAttachments = service.ServiceAttachments.Select(sa => sa.ToServiceAttachmentDTO()),
                Description = service.Description,
                PromoCode = service.PromoCode?.ToPromoCodeDTO(),
                Title = service.Title,
                IsDeleted = service.IsDeleted,
                Price = service.Price,
                VendorId = service.VendorId,
                Id = service.Id,
                ReviewsCount = service.Reviews.Count,
                AverageRate = service.Reviews.Any() ? Math.Round(service.Reviews.Select(r => r.Rate).Average()) : 0,
                ReservationsCount = service.Reservations.Count
            };
        }


        public static CustomerServiceDTO ToCustomerServiceDTO(this Service service)
        {
            return new CustomerServiceDTO
            {
                ServiceAttachments = service.ServiceAttachments.Select(sa => sa.ToServiceAttachmentDTO()),
                Description = service.Description,
                Title = service.Title,
                ReservationsCount = service.Reservations.Count,
                ReviewsCount = service.Reviews.Count,
                UserId = service.Vendor.UserId,
                Price = service.Price,
                VendorId = service.VendorId,
                Id = service.Id,
                AverageRate = service.Reviews.Any() ? Math.Round(service.Reviews.Select(r => r.Rate).Average()) : 0,
            };
        }

        //Temp
        //public static ShowAllServicesViewModel ToShowAllServicesViewModel(this Service Data)
        //{
        //    return new ShowAllServicesViewModel
        //    {
        //        Description = Data.Description,
        //        Id = Data.Id,
        //        IsDeleted = Data.IsDeleted,
        //        Price = Data.Price,
        //        PromoCode = Data.PromoCode,
        //        Reservations = Data.Reservations.Select(r => r.ToAdminReservationViewModel()),
        //        ReviewsCount = Data.Reviews.Count,
        //        ServiceAttachments = Data.ServiceAttachments.Select(sa => sa.ToViewModel()),
        //        Title = Data.Title,
        //        VendorId = Data.VendorId,
        //    };
        //}
    }
}