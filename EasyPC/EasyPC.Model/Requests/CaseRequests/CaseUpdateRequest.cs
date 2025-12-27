namespace EasyPC.Model.Requests.CaseRequests;

public class CaseUpdateRequest
{
    public string? Name { get; set; }

    public int Price { get; set; }

    public string? FormFactor { get; set; }

    public int ManufacturerId { get; set; }
}
