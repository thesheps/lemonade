﻿using System.Collections.Generic;

namespace Lemonade.Web.Models
{
    public class ApplicationsModel
    {
        public IList<ApplicationModel> Applications { get; set; }
        public IList<FeatureModel> Features { get; set; } 
    }
}