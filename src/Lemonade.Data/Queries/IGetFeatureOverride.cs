using Lemonade.Data.Entities;

namespace Lemonade.Data.Queries
{
    public interface IGetFeatureOverride
    {
        FeatureOverride Execute(int featureId, string hostname);
    }
}