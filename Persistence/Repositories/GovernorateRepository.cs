using Application.DTOs.GovernorateDTOs;
using Application.ExtensionMethods;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class GovernorateRepository : IGovernorateRepository
    {
        private readonly DbSet<Governorate> _governorates;

        public GovernorateRepository(EntitiesContext entitiesContext)
        {
            _governorates = entitiesContext.Governorates;
        }

        public IEnumerable<GovernorateDTO> Get()
        {
            return _governorates.Select(g => g.ToGovernorateDTO());
        }
    }
}
