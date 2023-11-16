using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using System.Globalization;
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
            Reservation Reservation = Data.ToReservation();
            Service? ReservedService = ServiceManager.Get(Data.ServiceId);
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
                Reservation.Price -= PromoCode.Discount;
                return EntitiesContext.Add(Reservation);
            }
        }
        public EntityEntry<Reservation>? ChangeStatus(int ReservationId, char Status)
        {
            Reservation? Reservation = Get(ReservationId);
            if (Reservation == null) return null;
            Reservation.Status = Status;
            return EntitiesContext.Update(Reservation);
        }
        public EntityEntry<Reservation>? Confirm(int ReservationId)
        {
            Reservation? Reservation = Get(ReservationId);
            if (Reservation == null) return null;
            Reservation.Status = 'c';
            return EntitiesContext.Update(Reservation);
        }
        public IQueryable<UserReservationViewModel> GetUserReservations(string UserId)
        {
            return Get()
                .Where(r => r.UserId == UserId)
                .Select(r => r.ToUserReservationViewModel());


        }
        public IQueryable<UserReservationViewModel> GetUserReservationsByIdAndStatus(string UserId, char Status)
        {
            return Get()
                .Where(r => r.UserId == UserId && r.Status == Status)
                .Select(r => r.ToUserReservationViewModel());
        }
        public IQueryable<VendorReservationViewModel> GetVendorReservations(int VendorId)
        {
            return Get()
                .Where(r => r.Service.VendorId == VendorId)
                .Select(r => r.ToVendorReservationViewModel());


        }
        public IQueryable<VendorReservationViewModel> GetVendorReservationsByIdAndStatus(int VendorId, char Status)
        {
            return Get()
                .Where(r => r.Service.VendorId == VendorId && r.Status == Status)
                .Select(r => r.ToVendorReservationViewModel());
        }
        public CheckoutReservationViewModel? CheckoutReservationById(string UserId, int ReservationId)
        {
            return Get()
                .Where(r => r.UserId == UserId && r.Id == ReservationId && r.Status == 'a')
                .Select(r => r.ToCheckoutReservationViewModel())
                .FirstOrDefault();
        }

        public Dictionary<string, int> GetReservationTotalDoneOrders(int vendorId, int year)
        {
            var reservations = Get()
                .Where(r => r.Service.VendorId == vendorId && r.Status == 'd' && r.DateTime.Year == year);

            var totalOrders = new Dictionary<string, int>();
            for (int i = 0; i < CultureInfo.CurrentCulture.DateTimeFormat.MonthNames.Length; i++)
            {
                var month = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[i];
                totalOrders.Add(month, reservations.Where(r => r.DateTime.Month == i).Count());
            }
            //foreach (var month in CultureInfo.CurrentCulture.DateTimeFormat.MonthNames)
            //{
            //    totalOrders.Add(month, 0);
            //}
            //foreach (var reservation in reservations)

            //{
            //    var monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(reservation.DateTime.Month);
            //    totalOrders[monthName]++;
            //}


            return totalOrders;
        }
        public Dictionary<string, float> GetReservationTotalSales(int vendorId, int year)

        {
            var reservations = Get()
                .Where(r => r.Service.VendorId == vendorId && r.Status == 'd' && r.DateTime.Year == year);

            var totalSales = new Dictionary<string, float>();
            for (int i = 0; i < CultureInfo.CurrentCulture.DateTimeFormat.MonthNames.Length; i++)
            {
                var month = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[i];
                totalSales.Add(month, reservations.Where(r => r.DateTime.Month == i).Sum(r=>r.Price));
            }
            //foreach (var month in CultureInfo.CurrentCulture.DateTimeFormat.MonthNames)
            //{
            //    totalSales.Add(month, 0);
            //}

            //foreach (var reservation in reservations)
            //{
            //    var monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(reservation.DateTime.Month);
            //    totalSales[monthName] += reservation.Price;
            //}

            return totalSales;
        }
    }
}