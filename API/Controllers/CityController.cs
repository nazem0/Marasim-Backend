using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly CityRepository CityManager;
        public CityController(CityRepository cityManager)
        {
            this.CityManager = cityManager;
        }
        [HttpGet("Get")]
        public IActionResult Get()
        {
            return Ok(CityManager.Get());
        }
        [HttpGet("GetByGovId/{GovId}")]
        public IActionResult Get(int GovId)
        {
            return Ok(CityManager.GetByGovId(GovId));
        }
    }
}
