namespace EasyPC.Model.Requests.GraphicsCardRequests;

public class GraphicsCardUpdateRequest
{
    public string? Name { get; set; }

    public string? VRAM { get; set; }

    public int Price { get; set; }

    public int ManufacturerId { get; set; }
}
