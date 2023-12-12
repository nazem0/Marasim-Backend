using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using Repository;
using ViewModels.CategoryViewModels;

namespace Marasim_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryRepository CategoryManager;
        public CategoryController(CategoryRepository _CategoryManager)
        {
            CategoryManager = _CategoryManager;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var Data = CategoryManager.Get();
            return Ok(Data);
        }

        [HttpGet("GetNames")]
        public IActionResult GetNames()
        {
            IEnumerable<CategoryNameViewModel> Categories = CategoryManager.GetNames();
            return Ok(Categories);
        }

        [HttpGet("GetCategoriesWithMinMax")]
        public IActionResult GetCategoriesWithMinMax()
        {
            return Ok(CategoryManager.GetCategoriesWithMinMax());
        }

        [HttpGet("Count")]
        public IActionResult Count()
        {
            return Ok(CategoryManager.Count());
        }

        [HttpGet("GetById/{Id}")]
        public IActionResult GetById(int Id)
        {
            Category? category = CategoryManager.Get(Id);
            if (category is null)
                return NotFound();
            return Ok(category.ToCategoryViewModel());
        }

        [HttpGet("GetByVendorId/{Id}")]
        public IActionResult GetByVendorId(int Id)
        {
            var x = CategoryManager.GetByVendorId(Id);
            return Ok(x);
        }

        [HttpPost("Add")]
        public IActionResult Add([FromForm] AddCategoryViewModel Data)
        {
            if (!ModelState.IsValid)
            {
                List<ModelError> Errors = new();
                foreach (var item in ModelState.Values)
                {
                    foreach (ModelError item1 in item.Errors)
                    {
                        Errors.Add(item1);
                    }
                }
                return BadRequest(Errors);
            }
            else
            {
                EntityEntry<Category>? Entry = CategoryManager.Add(Data);

                if (Entry is null)
                    return BadRequest("This Category Already Exists");
                else if (Entry.State != EntityState.Added)
                    return BadRequest(Entry.State);
                else
                {
                    CategoryManager.Save();
                    return Ok();
                }
            }
        }
    }
}
