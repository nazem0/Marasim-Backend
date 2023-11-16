﻿using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using ViewModels.PaginationViewModels;
using ViewModels.PaymentViewModel;

namespace Repository
{
    public class PaymentManager : MainManager<Payment>
    {
        private readonly EntitiesContext EntitiesContext;
        public PaymentManager(EntitiesContext _dBContext, EntitiesContext entitiesContext) : base(_dBContext)
        {
            EntitiesContext = entitiesContext;
        }
        public IEnumerable<PaymentViewModel> GetPayments()
        {
            return
                EntitiesContext.Payments
                .Select(p => p.ToPaymentViewModel());
        }
        public IEnumerable<PaymentViewModel> GetUnconfirmed()
        {
            return
                EntitiesContext.Payments
                .Where(p => p.Reservation.Status == 'f')
                .Select(p => p.ToPaymentViewModel());
        }
        public IEnumerable<PaymentViewModel> GetConfirmed()
        {
            return
                EntitiesContext.Payments
                .Where(p => p.Reservation.Status != 'f')
                .Select(p => p.ToPaymentViewModel());
        }
        public EntityEntry<Payment> Add(AddPaymentViewModel Data)
        {
            return EntitiesContext.Add(Data.ToPayment());
        }
        public PaginationViewModel<VendorPaymentViewModel> GetVendorsPayment(int VendorId,int PageIndex,int PageSize = 2)
        {
            PaginationDTO<Payment, VendorPaymentViewModel> PaginationDTO = new()
            {
                Filter = p => p.Reservation.Service.VendorId == VendorId,
                PageIndex = PageIndex,
                Selector = s => s.ToVendorPaymentViewModel(),
                PageSize = PageSize
            };
            return Get().ToPaginationViewModel(PaginationDTO);
        }
    }
}
