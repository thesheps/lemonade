using System.Collections.Generic;

namespace Lemonade.Web.Contracts
{
    public class Feature
    {
        public int FeatureId { get; set; }
        public int ApplicationId { get; set; }
        public Application Application { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public List<FeatureOverride> FeatureOverrides { get; set; }
    }
}