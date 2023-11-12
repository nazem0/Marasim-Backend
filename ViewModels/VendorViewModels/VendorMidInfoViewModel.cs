namespace ViewModels.VendorViewModels
{
    public class VendorMidInfoViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string PicUrl { get; set; }
        public required string UserId { get; set; }
        public required string Summary { get; set; }
        public required string CategoryName { get; set; }
    }
}

