using Application.DTOs.CategoryDTOs;
using Domain.Entities;
using System.Net;

namespace Application.Interfaces.IRepositories
{
    public interface ICategoryRepository
    {
        public IEnumerable<CategoryDTO> Get();
        public IEnumerable<CategoryWithVendorsDTO> GetCategoryWithVendors();
        public Category? GetByVendorId(int vendorId);
        public IEnumerable<CategoryMaxMinPriceDTO> GetCategoryMaxMinPrice();
        public HttpStatusCode Add(CreateCategoryDTO createCategoryDTO);
        public CategoryDTO? GetById(int id);
        public int Count();
    }
}