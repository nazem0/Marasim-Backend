using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.CategoryViewModels
{
    public static class CategoryExtensions
    {
        public Category ToCategory(this AddCategoryViewModel Data)
        {
            return new Category {  }
        }
    }
}
