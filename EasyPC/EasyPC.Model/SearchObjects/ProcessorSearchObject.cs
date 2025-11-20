namespace EasyPC.Model.SearchObjects
{
    public class ProcessorSearchObject : BaseSearchObject
    {
        public  string? Name { get; set; }
        public  string? Socket { get; set; }
        public  int? Price { get; set; }
        public  int? CoreCount { get; set; }
        public  int? ThreadCount { get; set; }
    }
}
