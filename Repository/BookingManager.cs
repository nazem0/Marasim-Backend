using Models;

namespace Repository
{
    public class BookingManager : MainManager<Booking>
    {
        public BookingManager(EntitiesContext _dBContext) : base(_dBContext)
        {
        }
    }
}
