namespace Lemonade.Web.Core.Events
{
    public class ApplicationHasBeenUpdated : IDomainEvent
    {
        public int ApplicationId { get; }
        public string Name { get; }

        public ApplicationHasBeenUpdated(int applicationId, string name)
        {
            ApplicationId = applicationId;
            Name = name;
        }
    }
}