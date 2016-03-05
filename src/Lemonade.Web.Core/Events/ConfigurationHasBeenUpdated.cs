namespace Lemonade.Web.Core.Events
{
    public class ConfigurationHasBeenUpdated : IDomainEvent
    {
        public int ConfigurationId { get; }
        public string Name { get; }
        public string Value { get; }

        public ConfigurationHasBeenUpdated(int configurationId, string name, string value)
        {
            ConfigurationId = configurationId;
            Name = name;
            Value = value;
        }
    }
}