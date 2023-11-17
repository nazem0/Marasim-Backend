using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.PaginationViewModels
{
    public static class PaginationExtensions
    {
        public static PaginationViewModel<TOut> ToPaginationViewModel<T, TOut>(this IQueryable<T> Data, PaginationDTO<T, TOut> PaginationDTO)
        {
            if (PaginationDTO.Filter is not null)
            {
                foreach (var filter in PaginationDTO.Filter)
                {
                    Data = Data.Where(filter);
                }
            }
            int Count = Data.Count();
            int ToBeSkipped = (PaginationDTO.PageIndex - 1) * PaginationDTO.PageSize;
            var Result = Data.Skip(ToBeSkipped).Take(PaginationDTO.PageSize);
            int Max = Convert.ToInt32(Math.Ceiling((double)Count / PaginationDTO.PageSize));
            return new PaginationViewModel<TOut>
            {
                Data = Result.Select(PaginationDTO.Selector),
                Count = Count,
                LastPage = Max,
                PageIndex = PaginationDTO.PageIndex,
                PageSize = PaginationDTO.PageSize
            };
        }
    }
}
