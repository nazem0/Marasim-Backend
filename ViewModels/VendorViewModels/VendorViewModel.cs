namespace ViewModels.VendorViewModels
{
    public class VendorViewModel
    {
        public required string UserID { get; set; }
        public required string Summary { get; set; }
        public required decimal Latitude { get; set; }
        public required decimal Longitude { get; set; }
        public required string Address { get; set; }
        public required int CategoryId { get; set; }
        public required string ExternalUrl { get; set; }
        public required string Name { get; set; }
        public required string PicUrl { get; set; }
        public required bool Gender { get; set; }
        public required string NationalID { get; set; }
    }
}

