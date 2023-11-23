namespace ViewModels.PaginationViewModels
{
    public static class PaginationExtensions
    {
        public static PaginationViewModel<T> ToPaginationViewModel<T>(this IQueryable<T> Data, PaginationDTO<T> PaginationDTO)
        {
            int Count = Data.Count();
            int ToBeSkipped = (PaginationDTO.PageIndex - 1) * PaginationDTO.PageSize;
            var Result = Data.Skip(ToBeSkipped).Take(PaginationDTO.PageSize);
            int Max = Convert.ToInt32(Math.Ceiling((double)Count / PaginationDTO.PageSize));
            return new PaginationViewModel<T>
            {
                Data = Result,
                Count = Count,
                LastPage = Max,
                PageIndex = PaginationDTO.PageIndex,
                PageSize = PaginationDTO.PageSize
            };
        }
    }
}
