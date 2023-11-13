using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GovernorateController : ControllerBase
    {
        private readonly GovernorateManager GovernorateManager;
        public GovernorateController(GovernorateManager governorateManager)
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
