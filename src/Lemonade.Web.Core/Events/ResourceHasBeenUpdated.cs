namespace Lemonade.Web.Core.Events
{
    public class ResourceHasBeenUpdated : IDomainEvent
    {
        public int ResourceId { get; }
        public int ApplicationId { get; }
        public int LocaleId { get; set; }
        public string ResourceSet { get; }
        public string ResourceKey { get; }
        public string Value { get; }

        public ResourceHasBeenUpdated(int resourceId, int applicationId, int localeId, string resourceSet, string resourceKey, string value)
        {
            ResourceId = resourceId;
            ApplicationId = applicationId;
            LocaleId = localeId;
            ResourceSet = resourceSet;
            ResourceKey = resourceKey;
            Value = value;
        }
    }
}