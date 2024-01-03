namespace Application.DTOs.ReservationDTOs
{
    public class ChangeReservationStatusDTO
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
    }

    public class CustomerChangeReservationStatusDTO
    {
        public required int Id { get; set; }
        public required string UserId { get; set; }
    }
}
