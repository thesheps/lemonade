using Lemonade.Domain.Infrastructure;

namespace Lemonade.Domain.Events
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