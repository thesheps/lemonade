namespace Lemonade.Web.Core.Events
{
    public class ConfigurationHasBeenUpdated : IDomainEvent
    {
        public int ConfigurationId { get; }
        public string Name { get; }

        public ConfigurationHasBeenUpdated(int configurationId, string name)
        {
            ConfigurationId = configurationId;
            Name = name;
        }
    }
}