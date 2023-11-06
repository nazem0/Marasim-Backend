using Models;
using ViewModels.VendorViewModels;

namespace ViewModels.CategoryViewModels
{
    public class CategoryViewModel
    {
        public required string Name { get; set; }
        public bool IsDeleted { get; set; }
        public IEnumerable<VendorViewModel>? Vendors { set; get; }
    }
}

