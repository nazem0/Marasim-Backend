using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GovernorateController : ControllerBase
    {
        private readonly GovernorateRepository GovernorateManager;
        public GovernorateController(GovernorateRepository governorateManager)
        {
            this.GovernorateManager = governorateManager;
        }
        [HttpGet("Get")]
        public IActionResult Get()
        {
            return Ok(GovernorateManager.Get());
        }
    }
}
