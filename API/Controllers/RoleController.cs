using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RoleController : ControllerBase
    {
        readonly RoleManager<IdentityRole> RoleManager;
        public RoleController(RoleManager<IdentityRole> _RoleManager)
        {
            RoleManager = _RoleManager;
        }
        [HttpPost]
        public async Task<IdentityResult> AddRole(string RoleName)
        {
           return await  RoleManager.CreateAsync(
                new IdentityRole
                {
                    Name = RoleName,
                });
        }
    }
}
