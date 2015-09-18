﻿using System;

namespace Lemonade.Web.Models
{
    public class FeatureModel
    {
        public int Id { get; set; }
        public string FeatureName { get; set; }
        public int ApplicationId { get; set; }
        public int? ExpirationDays { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime StartDate { get; set; }
    }
}