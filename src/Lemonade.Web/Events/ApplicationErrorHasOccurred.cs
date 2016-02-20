namespace Lemonade.Web.Events
{
    public class ApplicationErrorHasOccurred : IDomainEvent
    {
        public string Message { get; }

        public ApplicationErrorHasOccurred(string message)
        {
            Message = message;
        }
    }
}