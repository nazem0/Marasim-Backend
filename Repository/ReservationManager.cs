using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using ViewModels.ReservationViewModels;

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
        public EntityEntry<Reservation>? Accept(AcceptReservation Data)
        {
            Reservation? Reservation = Get(Data.Id).FirstOrDefault();
            if(Reservation == null) return null;
            Reservation.Status = 'a';
            return EntitiesContext.Update(Reservation);
        }
    }
}
