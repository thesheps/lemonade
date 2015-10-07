using System;

namespace Lemonade.Web.Events
{
    public class FeatureHasBeenUpdated : IDomainEvent
    {
        public int FeatureId { get; }
        public int ApplicationId { get; }
        public string Name { get; }
        public DateTime StartDate { get; }
        public int? ExpirationDays { get; }
        public bool IsEnabled { get; }

        public FeatureHasBeenUpdated(int featureId, int applicationId, string name, DateTime startDate, int? expirationDays, bool isEnabled)
        {
            FeatureId = featureId;
            ApplicationId = applicationId;
            Name = name;
            StartDate = startDate;
            ExpirationDays = expirationDays;
            IsEnabled = isEnabled;
        }
    }
}