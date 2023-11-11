using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using ViewModels.ReservationViewModels;
using ViewModels.UserViewModels;

namespace Repository
{
    public class ReservationManager : MainManager<Reservation>
    {
        private readonly EntitiesContext EntitiesContext;
        private readonly PromoCodeManager PromoCodeManager;
        private readonly ServiceManager ServiceManager;
        public ReservationManager(EntitiesContext _dBContext, PromoCodeManager promoCodeManager, ServiceManager serviceManager) : base(_dBContext)
        {
            EntitiesContext = _dBContext;
            PromoCodeManager = promoCodeManager;
            ServiceManager = serviceManager;
        }
        public EntityEntry<Reservation>? Add(AddReservationViewModel Data)
        {
            float Discount = 0;
            Reservation Reservation = Data.ToReservation();
            Service? ReservedService = ServiceManager.Get(Data.ServiceId).FirstOrDefault();
            if (ReservedService is null)
                return null;
            Reservation.Price = ReservedService.Price;
            if (Data.PromoCode is null)
                return EntitiesContext.Add(Reservation);
            PromoCode? PromoCode = PromoCodeManager.GetPromoCodeByCode(Data.PromoCode, Data.ServiceId);
            if (PromoCode == null)
                return EntitiesContext.Add(Reservation);
            else
            {
                Reservation.Price -= Discount;
                return EntitiesContext.Add(Reservation);
            }
        }
        public EntityEntry<Reservation>? ChangeStatus(int ReservationId,char Status)
        {
            Reservation? Reservation = Get(ReservationId).FirstOrDefault();
            if (Reservation == null) return null;
            Reservation.Status = Status;
            return EntitiesContext.Update(Reservation);
        }
        public EntityEntry<Reservation>? Confirm(int ReservationId)
        {
            Reservation? Reservation = Get(ReservationId).FirstOrDefault();
            if (Reservation == null) return null;
            Reservation.Status = 'c';
            return EntitiesContext.Update(Reservation);
        }
        public IQueryable<UserReservationViewModel> GetUserReservations(string UserId)
        {
            return Get()
                .Where(r => r.UserId == UserId)
                .Include(r => r.Service)
                .ThenInclude(s => s.Vendor.User)
                .Select(r => r.ToUserReservationViewModel());


        }
        public IQueryable<UserReservationViewModel> GetUserReservationsByIdAndStatus(string UserId, char Status)
        {
            return Get()
                .Where(r => r.UserId == UserId && r.Status == Status)
                .Include(r => r.Service)
                .ThenInclude(s => s.Vendor.User)
                .Select(r => r.ToUserReservationViewModel());
        }
        public IQueryable<VendorReservationViewModel> GetVendorReservations(int VendorId)
        {
            return Get()
                .Include(r => r.Service)
                .Include(r => r.User)
                .Where(r => r.Service.VendorId == VendorId)
                .Select(r => r.ToVendorReservationViewModel());


        }
        public IQueryable<VendorReservationViewModel> GetVendorReservationsByIdAndStatus(int VendorId, char Status)
        {
            return Get()
                .Include(r => r.Service)
                .Include(r => r.User)
                .Where(r => r.Service.VendorId == VendorId && r.Status == Status)
                .Select(r => r.ToVendorReservationViewModel());
        }
        public CheckoutReservationViewModel? CheckoutReservationById(string UserId,int ReservationId)
        {
            return Get()
                .Where(r => r.UserId == UserId && r.Id == ReservationId && r.Status == 'a')
                .Include(r => r.Service)
                .ThenInclude(s => s.Vendor.User)
                .Select(r => r.ToCheckoutReservationViewModel())
                .FirstOrDefault();
        }
      
    }
}