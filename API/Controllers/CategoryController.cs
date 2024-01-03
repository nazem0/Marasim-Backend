using Application.DTOs.CategoryDTOs;
using Application.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace Marasim_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var Data = _categoryRepository.GetCategoryWithVendors();
            return Ok(Data);
        }

        [HttpGet("GetNames")]
        public IActionResult GetNames()
        {
            IEnumerable<CategoryDTO> Categories = _categoryRepository.Get();
            return Ok(Categories);
        }

        [HttpGet("GetCategoriesWithMinMax")]
        public IActionResult GetCategoriesWithMinMax()
        {
            return Ok(_categoryRepository.GetCategoryMaxMinPrice());
        }

        [HttpGet("Count")]
        public IActionResult Count()
        {
            return Ok(_categoryRepository.Count());
        }

        [HttpGet("GetById/{Id}")]
        public IActionResult GetById(int Id)
        {
            var category = _categoryRepository.GetById(Id);
            if (category is null)
                return NotFound();
            return Ok(category);
        }

        [HttpGet("GetByVendorId/{Id}")]
        public IActionResult GetByVendorId(int Id)
        {
            var category = _categoryRepository.GetByVendorId(Id);
            return Ok(category);
        }

        [HttpPost("Add")]
        public IActionResult Add([FromForm] CreateCategoryDTO Data)
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
                var result = _categoryRepository.Add(Data);
                if (result == HttpStatusCode.Conflict) return Conflict("This Category Already Exists");
                return Ok();
            }
        }
    }
}

