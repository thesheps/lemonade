namespace Lemonade.Core.Events
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