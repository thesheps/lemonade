using System;

namespace Lemonade.Data.Entities
{
    public class Feature
    {
        public int FeatureId { get; set; }
        public Application Application { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public int? ExpirationDays { get; set; }
        public DateTime StartDate { get; set; }
    }
}