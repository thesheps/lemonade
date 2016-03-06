using Lemonade.Web.Contracts;

namespace Lemonade.Web.Core.Queries
{
    public class GetFeatureByNameAndApplicationQuery : IQuery<Feature>
    {
        public string ApplicationName { get; }
        public string FeatureName { get; }

        public GetFeatureByNameAndApplicationQuery(string applicationName, string featureName)
        {
            ApplicationName = applicationName;
            FeatureName = featureName;
        }
    }
}