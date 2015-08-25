using System;

namespace Lemonade.Data.Entities
{
    public class Feature
    {
        public bool? IsEnabled { get; set; }
        public int? ExpirationDays { get; set; }
        public DateTime StartDate { get; set; }
    }
}