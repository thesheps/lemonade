namespace Lemonade.Core.Events
{
    public class ApplicationHasBeenSaved : IDomainEvent
    {
        public int ApplicationId { get; }
        public string Name { get; }

        public ApplicationHasBeenSaved(int applicationId, string name)
        {
            ApplicationId = applicationId;
            Name = name;
        }
    }
}