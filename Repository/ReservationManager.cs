using Models;

namespace Repository
{
    public class ReservationManager : MainManager<Reservation>
    {
        public ReservationManager(EntitiesContext _dBContext) : base(_dBContext)
        {
        }
    }
}
