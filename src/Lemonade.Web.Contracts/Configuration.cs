namespace Lemonade.Web.Contracts
{
    public class Configuration
    {
        public int ConfigurationId { get; set; }
        public int ApplicationId { get; set; }
        public Application Application { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}