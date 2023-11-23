namespace ViewModels.PaginationViewModels
{
    public class PaginationDTO<T>
    {
        public int PageSize = 5;
        public required int PageIndex = 1;
    }
}
