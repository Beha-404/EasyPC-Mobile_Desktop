namespace EasyPC.Model.Requests.PowerSupplyRequests;

public class PowerSuplyUpdateRequest
{
    public string? Name { get; set; }

    public string? Power { get; set; }

    public int Price { get; set; }

    public int ManufacturerId { get; set; }
}
