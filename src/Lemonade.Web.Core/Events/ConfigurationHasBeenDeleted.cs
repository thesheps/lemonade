namespace Lemonade.Web.Core.Events
{
    public class ConfigurationHasBeenDeleted : IDomainEvent
    {
        public int ConfigurationId { get; }

        public ConfigurationHasBeenDeleted(int configurationId)
        {
            ConfigurationId = configurationId;
        }
    }
}