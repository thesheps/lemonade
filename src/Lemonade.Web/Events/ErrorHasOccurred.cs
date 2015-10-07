namespace Lemonade.Web.Events
{
    public class ErrorHasOccurred : IDomainEvent
    {
        public string ErrorMessage { get; }

        public ErrorHasOccurred(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}