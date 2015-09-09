using System;

namespace Lemonade.Web.Contracts
{
    public class FeatureModel
    {
        public int Id { get; set; }
        public string FeatureName { get; set; }
        public string ApplicationName { get; set; }
        public int? ExpirationDays { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime StartDate { get; set; }
    }
}