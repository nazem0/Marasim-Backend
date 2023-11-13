using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> RoleManager;
        public RoleController(RoleManager<IdentityRole> _RoleManager)
        {
            RoleManager = _RoleManager;
        }

        [HttpPost("Add")]
        public async Task<IdentityResult> Add(string RoleName)
        {
            return await RoleManager.CreateAsync(
                 new IdentityRole
                 {
                     Name = RoleName,
                 });
        }
        [HttpGet("Get")]
        public IActionResult Get()
        {
            return Ok(RoleManager.Roles);
        }
    }
}
