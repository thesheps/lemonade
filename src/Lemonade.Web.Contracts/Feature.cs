using System;

namespace Lemonade.Web.Contracts
{
    public class Feature
    {
        public int Id { get; set; }
        public string FeatureName { get; set; }
        public string ApplicationName { get; set; }
        public int? ExpirationDays { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime StartDate { get; set; }
    }
}