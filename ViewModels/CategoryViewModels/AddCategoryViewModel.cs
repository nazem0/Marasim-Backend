﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.CategoryViewModels
{
    public class AddCategoryViewModel
    {
        public required string Name { get; set; }
        public required string PicUrl { get; set; }
        public bool IsDeleted { get; set; }
    }
}
