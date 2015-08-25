using System;

namespace Lemonade.Web.Models
{
    public class FeaturesModel
    {
        public int? ExpirationDays { get; set; }
        public bool? IsEnabled { get; set; }
        public DateTime StartDate { get; set; }
    }
}