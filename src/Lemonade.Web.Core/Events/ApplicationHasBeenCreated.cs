namespace Lemonade.Web.Core.Events
{
    public class ApplicationHasBeenCreated : IDomainEvent
    {
        public int ApplicationId { get; }
        public string Name { get; }

        public ApplicationHasBeenCreated(int applicationId, string name)
        {
            ApplicationId = applicationId;
            Name = name;
        }
    }
}