namespace Lemonade.Web.Events
{
    public class FeatureErrorHasOccurred : IDomainEvent
    {
        public string ErrorMessage { get; }

        public FeatureErrorHasOccurred(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}