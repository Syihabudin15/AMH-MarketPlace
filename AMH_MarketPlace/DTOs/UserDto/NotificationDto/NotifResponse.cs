namespace AMH_MarketPlace.DTOs.UserDto.NotificationDto
{
    public class NotifResponse
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public string? CreatedAt { get; set; }
        public bool isRead { get; set; }
        public string? Category { get; set; } 
    }
}
