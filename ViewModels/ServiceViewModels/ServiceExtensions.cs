﻿using Models;
using ViewModels.PromoCodeViewModels;
using ViewModels.ReservationViewModels;
using ViewModels.ServiceAttatchmentViewModels;

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
        public static ServiceViewModel ToServiceViewModel(this Service Data, string VendorUserId)
        {
            return new ServiceViewModel
            {
                UserId = VendorUserId,
                ServiceAttachments = Data.ServiceAttachments.Select(sa => sa.ToViewModel()),
                Description = Data.Description,
                PromoCode = Data.PromoCode?.ToPromoCodeViewModel(),
                Title = Data.Title,
                IsDeleted = Data.IsDeleted,
                Price = Data.Price,
                VendorId = Data.VendorId,
                Id = Data.Id,
                ReviewsCount = Data.Reviews.Count,
                Reservations = Data.Reservations.Select(r => r.ToVendorReservationViewModel()),
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
                Title = Service.Title
            };
        }

        public static ShowAllServicesViewModel ToShowAllServicesViewModel(this Service Data)
        {
            return new ShowAllServicesViewModel
            {
                Description = Data.Description,
                Id = Data.Id,
                IsDeleted = Data.IsDeleted,
                Price = Data.Price,
                PromoCode = Data.PromoCode?.ToPromoCodeViewModel(),
                Reservations = Data.Reservations.Select(r => r.ToAdminReservationViewModel()),
                ReviewsCount = Data.Reviews.Count,
                ServiceAttachments = Data.ServiceAttachments.Select(sa => sa.ToViewModel()),
                Title = Data.Title,
                VendorId = Data.VendorId,
            };
        }
    }
}