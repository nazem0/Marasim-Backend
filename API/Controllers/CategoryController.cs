using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using Repository;
using ViewModels.CategoryViewModels;
using ViewModels.VendorViewModels;

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
            var Data = CategoryManager.Get();
            return new JsonResult(Data);
        }
        [HttpGet("Count")]
        public IActionResult Count()
        {
            return Ok(CategoryManager.Count());
        }
        [HttpGet("GetById/{ID}")]
        public IActionResult GetById(int ID)
        {
            var x = CategoryManager.Get(ID).FirstOrDefault();
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
                    return Ok($"Category {Entry.Entity.Name} Created Successfully With The Id {Entry.Entity.Id}");
                }
            }
        }
    }
}
