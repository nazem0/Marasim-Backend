namespace Application.DTOs.CategoryDTOs
{
    public class CategoryMaxMinPriceDTO
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required float Min { get; set; }
        public required float Max { get; set; }
    }
}
