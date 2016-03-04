namespace Lemonade.Web.Core.Events
{
    public class ResourceHasBeenUpdated : IDomainEvent
    {
        public int ResourceId { get; }
        public int ApplicationId { get; }
        public string ResourceSet { get; }
        public string ResourceKey { get; }
        public string Locale { get; }
        public string Value { get; }

        public ResourceHasBeenUpdated(int resourceId, int applicationId, string resourceSet, string resourceKey, string locale, string value)
        {
            ResourceId = resourceId;
            ApplicationId = applicationId;
            ResourceSet = resourceSet;
            ResourceKey = resourceKey;
            Locale = locale;
            Value = value;
        }
    }
}