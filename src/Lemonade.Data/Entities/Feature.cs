using System;

namespace Lemonade.Core.Entities
{
    public class Feature
    {
        public int FeatureId { get; set; }
        public int ApplicationId { get; set; }
        public Application Application { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public int? ExpirationDays { get; set; }
        public DateTime StartDate { get; set; }
    }
}