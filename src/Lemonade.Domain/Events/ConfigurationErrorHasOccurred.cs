using Lemonade.Domain.Infrastructure;

namespace Lemonade.Domain.Events
{
    public class ConfigurationErrorHasOccurred : IDomainEvent
    {
        public string Message { get; private set; }

        public ConfigurationErrorHasOccurred(string message)
        {
            Message = message;
        }
    }
}