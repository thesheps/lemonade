namespace Lemonade.Web.Core.Events
{
    public class ConfigurationErrorHasOccurred : IDomainEvent
    {
        public string Message { get; }

        public ConfigurationErrorHasOccurred(string message)
        {
            Message = message;
        }
    }
}