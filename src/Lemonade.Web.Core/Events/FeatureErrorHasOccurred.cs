namespace Lemonade.Web.Core.Events
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