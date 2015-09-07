using System;

namespace Lemonade.Web.Models
{
    public class FeatureModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Application { get; set; }
        public int? ExpirationDays { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime StartDate { get; set; }
    }
}