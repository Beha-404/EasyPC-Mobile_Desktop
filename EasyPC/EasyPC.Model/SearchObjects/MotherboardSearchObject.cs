namespace EasyPC.Model.SearchObjects
{
    public class MotherboardSearchObject : BaseSearchObject
    {
        public  string? Name { get; set; }
        public  string? Socket { get; set; }
        public  int? Price { get; set; }
        public bool? SupportsOverclocking { get; set; }
    }
}
