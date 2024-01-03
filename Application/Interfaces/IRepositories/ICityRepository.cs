using Application.DTOs.CityDTOs;

namespace Application.Interfaces.IRepositories
{
    public interface ICityRepository
    {
        public IEnumerable<CityDTO> Get();
        public IEnumerable<CityDTO> GetByGovId(int govId);
    }
}
