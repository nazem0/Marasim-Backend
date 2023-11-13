using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.GovernorateViewModels;

namespace Repository
{
    public class GovernorateManager : MainManager<Governorate>
    {
        private readonly EntitiesContext EntitiesContext;

        public GovernorateManager(EntitiesContext _dBContext) : base(_dBContext)
        {
            EntitiesContext = _dBContext;
        }

        public new IEnumerable<GovernorateViewModel> Get()
        {
            return EntitiesContext.Governorates.Select(g => g.ToGovernorateViewModel());
        }
    }
}
