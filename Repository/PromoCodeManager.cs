using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;

namespace Repository
{
    public class PromoCodeManager : MainManager<PromoCode>
    {
        private readonly EntitiesContext EntitiesContext;
        public PromoCodeManager(EntitiesContext _dBContext) : base(_dBContext)
        {
            EntitiesContext = _dBContext;
        }
        public EntityEntry<PromoCode> Add(PromoCode entity)
        {
            return EntitiesContext.Add(entity);
        }
        public PromoCode GetPromoCodeByID(int Id)
        {
            return Get().Where(p => p.Id == Id).FirstOrDefault()!;
        }
        public PromoCode? GetPromoCodeByCode(string Code,int ServiceId)
        {
            return Get().Where(pc => pc.Code == Code & pc.ServiceId==ServiceId).FirstOrDefault();
        }

    }
}
