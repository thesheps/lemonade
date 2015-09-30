using System;

namespace Lemonade.Core.Events
{
    public class FeatureHasBeenSaved : IDomainEvent
    {
        public int FeatureId { get; }
        public int ApplicationId { get; }
        public string Name { get; }
        public DateTime StartDate { get; }
        public int? ExpirationDays { get; }
        public bool IsEnabled { get; }

        public FeatureHasBeenSaved(int featureId, int applicationId, string name, DateTime startDate, int? expirationDays, bool isEnabled)
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