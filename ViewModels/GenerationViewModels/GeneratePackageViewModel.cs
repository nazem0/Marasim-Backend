namespace ViewModels.GenerationViewModels
{
    public class GeneratePackageViewModel
    {
        public required float Budget { get; set; }
        public required CategoryPrice[] CategoryPrice { get; set; }
        public int GovId { get; set; }
        public int? CityId { get; set; } = null;
        public string? District { get; set; } = null;
        public int? Rate { get; set; } = null;
    }
    public class CategoryPrice
    {
        public required int CategoryId { get; set; }
        public required int Price { get; set; }
    }
}
