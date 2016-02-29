namespace Lemonade.Data.Entities
{
    public class Resource
    {
        public int ResourceId { get; set; }
        public int ApplicationId { get; set; }
        public string ResourceSet { get; set; }
        public string ResourceKey { get; set; }
        public string Locale { get; set; }
        public string Value { get; set; }
        public Application Application { get; set; }
    }
}