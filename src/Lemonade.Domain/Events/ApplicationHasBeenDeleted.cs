using Lemonade.Domain.Infrastructure;

namespace Lemonade.Domain.Events
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