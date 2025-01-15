using Application.DTOs.CategoryDTOs;
using Application.ExtensionMethods;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DbSet<Category> _categories;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryRepository(AppDbContext entitiesContext, IUnitOfWork unitOfWork)
        {
            _categories = entitiesContext.Categories;
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<CategoryDTO> Get()
        {
            return _categories.Select(c => c.ToCategoryDTO());
        }
        public CategoryDTO? GetById(int id)
        {
            return _categories.Find(id)?.ToCategoryDTO();
        }
        public IEnumerable<CategoryWithVendorsDTO> GetCategoryWithVendors()
        {
            return
                _categories.Select(C => C.ToCategoryWithVendorsDTO());
        }

        public Category? GetByVendorId(int vendorId)
        {
            return _categories.Where(c => c.Vendors.Any(v => v.Id == vendorId)).FirstOrDefault();
        }
        public IEnumerable<CategoryMaxMinPriceDTO> GetCategoryMaxMinPrice()
        {
            return _categories.Select(c => c.ToCategoryMaxMinPriceDTO());
        }
        public HttpStatusCode Add(CreateCategoryDTO createCategoryDTO)
        {
            if (_categories.Where(c => c.Name == createCategoryDTO.Name).Any())
                return HttpStatusCode.Conflict;
            _categories.Add(createCategoryDTO.ToEntity());
            return _unitOfWork.SaveChanges();
        }

        public int Count()
        {
            return _categories.Count();
        }
    }
}