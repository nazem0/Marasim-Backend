using Microsoft.AspNetCore.Mvc;
using Repository;
namespace Marasim_Backend.Controllers
{
    public class Category : Controller
    {
        private CategoryManager CategoryManager { get; set; }
        public Category(CategoryManager _CategoryManager) {
            CategoryManager = _CategoryManager;
        }
        public IActionResult Index()
        {
            var x = CategoryManager.Get().ToList();
            return Json(x);
        }
    }
}
