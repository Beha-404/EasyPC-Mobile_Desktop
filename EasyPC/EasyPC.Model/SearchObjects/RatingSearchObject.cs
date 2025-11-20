namespace EasyPC.Model.SearchObjects
{
    public class RatingSearchObject : BaseSearchObject
    {
        public int? RatingValue { get; set; }
        public int? UserId { get; set; }
        public int? PcId { get; set; }
    }
}
