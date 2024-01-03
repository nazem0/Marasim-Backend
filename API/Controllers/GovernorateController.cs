using Application.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GovernorateController : ControllerBase
    {
        private readonly IGovernorateRepository _governorateRepository;
        public GovernorateController(IGovernorateRepository governorateRepository)
        {
            this._governorateRepository = governorateRepository;
        }
        [HttpGet("Get")]
        public IActionResult Get()
        {
            return Ok(_governorateRepository.Get());
        }
    }
}
