namespace EasyPC.Model.Requests.RatingRequests
{
    public class RatingInsertRequest
    {
        public int UserId { get; set; }
        public int PcId { get; set; }
        public int RatingValue { get; set; }
    }
}
