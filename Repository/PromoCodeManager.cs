using Models;

namespace Repository
{
    public class PromoCodeManager : MainManager<PromoCode>
    {
        public PromoCodeManager(EntitiesContext _dBContext) : base(_dBContext) { }

    }
}
