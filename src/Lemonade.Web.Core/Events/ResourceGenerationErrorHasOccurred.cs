namespace Lemonade.Web.Core.Events
{
    public class ResourceGenerationErrorHasOccurred : IDomainEvent
    {
        public string Message { get; }

        public ResourceGenerationErrorHasOccurred(string message)
        {
            Message = message;
        }
    }
}