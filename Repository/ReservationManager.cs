using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using ViewModels.ReservationViewModels;

namespace Repository
{
    public class ReservationManager : MainManager<Reservation>
    {
        private readonly EntitiesContext EntitiesContext;
        private readonly PromoCodeManager PromoCodeManager;
        public ReservationManager(EntitiesContext _dBContext, PromoCodeManager promoCodeManager) : base(_dBContext)
        {
            EntitiesContext = _dBContext;
            PromoCodeManager = promoCodeManager;
        }
        public EntityEntry<Reservation> Add(AddReservationViewModel Data)
        {

            float Discount = 0;
            Reservation Reservation = Data.ToReservation();
            Reservation.Price = Reservation.Service.Price;
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
    }
}
