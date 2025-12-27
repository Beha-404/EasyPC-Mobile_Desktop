namespace EasyPC.Model.Requests.ProcessorRequests;

public class ProcessorUpdateRequest
{
    public string? Name { get; set; }

    public string? Socket { get; set; }

    public int Price { get; set; }

    public int CoreCount { get; set; }

    public int ThreadCount { get; set; }

    public int ManufacturerId { get; set; }
}
