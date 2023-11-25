using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.CategoryViewModels
{
    public class CategoriesWithMinMaxViewModel
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required float Min { get; set; }
        public required float Max { get; set; }
    }
}
