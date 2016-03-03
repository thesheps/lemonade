using Lemonade.Web.Infrastructure;

namespace Lemonade.Web.Events
{
    public class ConfigurationHasBeenDeleted : IDomainEvent
    {
        public int ConfigurationId { get; private set; }

        public ConfigurationHasBeenDeleted(int configurationId)
        {
            ConfigurationId = configurationId;
        }
    }
}