using Models;
using ViewModels.GovernorateViewModels;

namespace Repository
{
    public class GovernorateRepository : BaseRepository<Governorate>
    {
        private readonly EntitiesContext EntitiesContext;

        public GovernorateRepository(EntitiesContext _dBContext) : base(_dBContext)
        {
            EntitiesContext = _dBContext;
        }

        public new IEnumerable<GovernorateViewModel> Get()
        {
            return EntitiesContext.Governorates.Select(g => g.ToGovernorateViewModel());
        }
    }
}
