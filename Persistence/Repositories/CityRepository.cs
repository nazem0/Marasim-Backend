using Application.DTOs.CityDTOs;
using Application.ExtensionMethods;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly DbSet<City> _cities;
        public CityRepository(AppDbContext entitiesContext)
        {
            _cities = entitiesContext.Cities;
        }
        public IEnumerable<CityDTO> Get()
        {
            return _cities.Select(c => c.ToCityDTO());
        }
        public IEnumerable<CityDTO> GetByGovId(int govId)
        {
            return _cities.Where(c => c.GovernorateId == govId).Select(c => c.ToCityDTO());
        }
    }
}
