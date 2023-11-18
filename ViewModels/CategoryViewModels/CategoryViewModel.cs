﻿using ViewModels.VendorViewModels;

namespace ViewModels.CategoryViewModels
{
    public class CategoryViewModel
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public bool IsDeleted { get; set; }
        public IEnumerable<VendorMidInfoViewModel>? Vendors { set; get; }
    }
}

