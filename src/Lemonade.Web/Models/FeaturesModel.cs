using System.Collections.Generic;

namespace Lemonade.Web.Models
{
    public class FeaturesModel
    {
        public int ApplicationId { get; }
        public IList<ApplicationModel> Applications { get; }
        public IList<FeatureModel> Features { get; }

        public FeaturesModel(int applicationId, IList<ApplicationModel> applications, IList<FeatureModel> features)
        {
            ApplicationId = applicationId;
            Applications = applications;
            Features = features;
        }
    }
}