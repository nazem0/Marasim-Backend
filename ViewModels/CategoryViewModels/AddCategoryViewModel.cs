using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.CategoryViewModels
{
    public class AddCategoryViewModel
    {
        [Required,MaxLength(30,ErrorMessage ="Category Name Cannot Exceed 30 Characters")]
        public required string Name { get; set; }
    }
}
