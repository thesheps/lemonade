namespace Lemonade.Web.Core.Events
{
    public class ResourcesHaveBeenGenerated : IDomainEvent
    {
        public int ApplicationId { get; }
        public int TargetLocaleId { get; }
        public string TranslationType { get; }

        public ResourcesHaveBeenGenerated(int applicationId, int targetLocaleId, string translationType)
        {
            ApplicationId = applicationId;
            TargetLocaleId = targetLocaleId;
            TranslationType = translationType;
        }
    }
}