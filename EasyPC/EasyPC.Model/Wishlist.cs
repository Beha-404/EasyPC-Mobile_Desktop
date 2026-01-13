namespace EasyPC.Model;

public class Wishlist
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int PcId { get; set; }

    public DateTime DateAdded { get; set; } = DateTime.UtcNow;

    public User? User { get; set; }

    public PC? PC { get; set; }
}
