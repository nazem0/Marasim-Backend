using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Marasim_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryManager CategoryManager;
        public CategoryController(CategoryManager _CategoryManager)
        {
            CategoryManager = _CategoryManager;
        }
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            var x = CategoryManager.Get().ToList();
            return new JsonResult(x);
        }
        [HttpGet("Count")]
        public IActionResult Count()
        {
            return Ok(CategoryManager.Count());
        }
        [HttpGet("GetById/{ID}")]
        public IActionResult GetById(int ID)
        {
            var x = CategoryManager.Get(ID);
            return new JsonResult(x);
        }
    }
}
