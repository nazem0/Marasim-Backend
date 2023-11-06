using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;

namespace Repository
{
    public class PromoCodeManager : MainManager<PromoCode>
    {
        private readonly EntitiesContext EntitiesContext;
        public PromoCodeManager(EntitiesContext _dBContext) : base(_dBContext) {
            EntitiesContext = _dBContext;
        }
        public EntityEntry<PromoCode> Add(PromoCode entity)
        {
            return EntitiesContext.Add(entity);
        }
        //public PromoCode GetPromoCodeByID(int ID)
        //{
        //    return Get().Where(p => p.ID == ID && p.IsDeleted == false).FirstOrDefault()!;
        //}

    }
}
