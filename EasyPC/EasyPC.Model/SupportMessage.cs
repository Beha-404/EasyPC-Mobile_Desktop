namespace EasyPC.Model
{
    public class SupportMessage
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? SenderName { get; set; }
        public string? Message { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsRead { get; set; }
        public User? User { get; set; }
    }
}
