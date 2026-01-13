namespace EasyPC.Model.Requests.OrderDetailsRequests;

public class OrderDetailsInsertRequest
{
    public int PcId { get; set; }

    public int Quantity { get; set; }

    public int UnitPrice { get; set; }
}
