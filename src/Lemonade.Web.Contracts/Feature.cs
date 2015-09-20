using System;

namespace Lemonade.Web.Contracts
{
    public class Feature
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public Application Application { get; set; }
        public string Name { get; set; }
        public int? ExpirationDays { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime StartDate { get; set; }
    }
}