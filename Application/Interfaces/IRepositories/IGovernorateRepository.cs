using Application.DTOs.GovernorateDTOs;

namespace Application.Interfaces.IRepositories
{
    public interface IGovernorateRepository
    {
        public IEnumerable<GovernorateDTO> Get();
    }
}
