using Models;
using ViewModels.CityViewModels;
namespace Repository
{
    public class CityManager : MainManager<City>
    {
        private readonly EntitiesContext EntitiesContext;
        public CityManager(EntitiesContext _dBContext) : base(_dBContext)
        {
            EntitiesContext = _dBContext;
        }
        public new IEnumerable<CityViewModel> Get()
        {
            return EntitiesContext.Cities.Select(c => c.ToCityViewModel());
        }
        public IEnumerable<CityViewModel> GetByGovId(int GovId)
        {
            return EntitiesContext.Cities.Where(c => c.GovernorateId == GovId).Select(c => c.ToCityViewModel());
        }
    }
}
