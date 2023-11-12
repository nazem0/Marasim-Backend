using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class PaginationViewModel<T>
    {
        public required IEnumerable<T> Data { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public int LastPage { get; set; }
    }
}
