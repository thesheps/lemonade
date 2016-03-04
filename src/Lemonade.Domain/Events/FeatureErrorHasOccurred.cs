using Lemonade.Domain.Infrastructure;

namespace Lemonade.Domain.Events
{
    public class FeatureErrorHasOccurred : IDomainEvent
    {
        public string Message { get; }

        public FeatureErrorHasOccurred(string message)
        {
            Message = message;
        }
    }
}