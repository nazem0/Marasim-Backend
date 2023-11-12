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
