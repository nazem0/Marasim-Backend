using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.PaginationViewModels
{
    public class PaginationDTO<T,TOut>
    {
        public IList<Expression<Func<T, bool>>>? Filter;
        public int PageSize = 5;
        public required int PageIndex = 1;
        public required Expression<Func<T, TOut>> Selector;
    }
}
