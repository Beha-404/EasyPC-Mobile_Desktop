namespace EasyPC.Model;

public class Rating
{
    public int Id { get; set; }
    public int RatingValue { get; set; }
    public int UserId { get; set; }
    public int PcId { get; set; }
}
