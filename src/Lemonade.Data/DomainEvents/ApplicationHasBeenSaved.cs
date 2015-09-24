namespace Lemonade.Core.DomainEvents
{
    public class ApplicationHasBeenSaved : IDomainEvent
    {
        public int ApplicationId { get; }

        public ApplicationHasBeenSaved(int applicationId)
        {
            ApplicationId = applicationId;
        }
    }
}