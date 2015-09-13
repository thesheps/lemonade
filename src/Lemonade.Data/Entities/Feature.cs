using System;

namespace Lemonade.Data.Entities
{
    public class Feature
    {
        public int Id { get; set; }
        public bool IsEnabled { get; set; }
        public int? ExpirationDays { get; set; }
        public DateTime StartDate { get; set; }
        public string FeatureName { get; set; }
        public string ApplicationName { get; set; }
    }
}