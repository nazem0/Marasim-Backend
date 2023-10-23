using Microsoft.AspNetCore.Mvc;
using Repository;
namespace Marasim_Backend.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryManager CategoryManager { get; set; }
        public CategoryController(CategoryManager _CategoryManager)
        {
            CategoryManager = _CategoryManager;
        }
        public IActionResult Index()
        {
            var x = CategoryManager.Get().ToList();
            return Json(x);
        }
    }
}
