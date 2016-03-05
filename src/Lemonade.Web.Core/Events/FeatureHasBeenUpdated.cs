namespace Lemonade.Web.Core.Events
{
    public class FeatureHasBeenUpdated : IDomainEvent
    {
        public int FeatureId { get; }
        public int ApplicationId { get; }
        public string Name { get; }
        public bool IsEnabled { get; }

        public FeatureHasBeenUpdated(int featureId, int applicationId, string name, bool isEnabled)
        {
            FeatureId = featureId;
            ApplicationId = applicationId;
            Name = name;
            IsEnabled = isEnabled;
        }
    }
}