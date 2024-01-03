using Application.DTOs.PaginationDTOs;

namespace Application.ExtensionMethods
{
    public static class EnumerableExtensions
    {
        public static PaginationDTO<T> ToPaginationDTO<T>
            (this IEnumerable<T> enumerableList, int pageIndex, int pageSize)
        {
            long count = enumerableList.LongCount();
            return new PaginationDTO<T>
            {
                Data = enumerableList.Skip((pageIndex - 1) * pageSize).Take(pageSize),
                Count = count,
                PageSize = pageSize,
                LastPage = Convert.ToInt32(Math.Ceiling((double)count / pageSize)),
                Length = pageSize,
                PageIndex = pageIndex
            };
        }

    }
}
