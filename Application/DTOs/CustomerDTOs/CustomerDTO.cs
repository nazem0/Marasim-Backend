namespace Application.DTOs.CustomerDTOs
{
    public class CustomerDTO
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string PicUrl { get; set; }
        public bool Gender { get; set; }
        public required string NationalId { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public required DateTime RegistrationDate { get; set; }
    }
}

