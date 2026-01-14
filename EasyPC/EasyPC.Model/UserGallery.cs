namespace EasyPC.Model
{
    public class UserGallery
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; }
        public string? Description { get; set; }
    }
}
