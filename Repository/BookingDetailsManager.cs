using Models;

namespace Repository
{
    public class BookingDetailsManager : MainManager<BookingDetails>
    {
        public BookingDetailsManager(EntitiesContext _dBContext) : base(_dBContext)
        {
        }
    }
}
