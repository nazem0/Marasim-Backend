namespace Application.DTOs.ReactDTOs
{
    public class ReactDTO
    {
        public required int Id { get; set; }
        public required string UserId { get; set; }
        public required int PostId { get; set; }
        public required DateTime DateTime { get; set; }
        public required string UserName { get; set; }
        public required string UserPicUrl { get; set; }
    }
}

