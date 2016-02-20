namespace Lemonade.Web.Events
{
    public class ConfigurationHasBeenCreated : IDomainEvent
    {
        public int ConfigurationId { get; private set; }
        public string Name { get; private set; }
        public string Value { get; private set; }

        public ConfigurationHasBeenCreated(int configurationId, string name, string value)
        {
            ConfigurationId = configurationId;
            Name = name;
            Value = value;
        }
    }
}