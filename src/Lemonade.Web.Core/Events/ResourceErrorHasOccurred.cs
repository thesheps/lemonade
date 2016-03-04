namespace Lemonade.Web.Core.Events
{
    public class ResourceErrorHasOccurred : IDomainEvent
    {
        public string Message { get; }

        public ResourceErrorHasOccurred(string message)
        {
            Message = message;
        }
    }
}