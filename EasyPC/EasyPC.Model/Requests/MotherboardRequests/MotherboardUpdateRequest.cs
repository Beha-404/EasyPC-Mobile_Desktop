namespace EasyPC.Model.Requests.MotherboardRequests;

public class MotherboardUpdateRequest
{
    public string? Name { get; set; }

    public string? Socket { get; set; }

    public int Price { get; set; }

    public int ManufacturerId { get; set; }
}
