using Models;

namespace Repository
{
    public class PromoCodeManager : MainManager<PromoCode>
    {
        public PromoCodeManager(EntitiesContext _dBContext) : base(_dBContext) { }

        //public PromoCode GetPromoCodeByID(int ID)
        //{
        //    return Get().Where(p => p.ID == ID && p.IsDeleted == false).FirstOrDefault()!;
        //}

    }
}
