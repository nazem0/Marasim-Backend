using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Marasim_Backend.Controllers
{
    public class CategoryController : ControllerBase
    {
        private CategoryManager CategoryManager { get; set; }
        public CategoryController(CategoryManager _CategoryManager)
        {
            CategoryManager = _CategoryManager;
        }
        public IActionResult Index()
        {
            var x = CategoryManager.Get().ToList();
            return new JsonResult(x);
        }
        public IActionResult Details(int ID)
        {
            var x = CategoryManager.Get(ID);
            return new JsonResult(x);
        }
    }
}
