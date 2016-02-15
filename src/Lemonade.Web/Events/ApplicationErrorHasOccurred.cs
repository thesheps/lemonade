namespace Lemonade.Web.Events
{
    public class ApplicationErrorHasOccurred : IDomainEvent
    {
        public string ErrorMessage { get; }

        public ApplicationErrorHasOccurred(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}