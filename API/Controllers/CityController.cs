using Application.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;
        public CityController(ICityRepository cityRepository)
        {
            this._cityRepository = cityRepository;
        }
        [HttpGet("Get")]
        public IActionResult Get()
        {
            return Ok(_cityRepository.Get());
        }
        [HttpGet("GetByGovId/{GovId}")]
        public IActionResult Get(int GovId)
        {
            return Ok(_cityRepository.GetByGovId(GovId));
        }
    }
}
