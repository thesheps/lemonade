namespace Lemonade.Web.Core.Events
{
    public class ApplicationHasBeenDeleted : IDomainEvent
    {
        public int ApplicationId { get; }

        public ApplicationHasBeenDeleted(int applicationId)
        {
            ApplicationId = applicationId;
        }
    }
}