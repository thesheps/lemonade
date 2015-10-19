using Lemonade.Data.Entities;

namespace Lemonade.Data.Queries
{
    public interface IUpdateFeatureOverride
    {
        void Execute(FeatureOverride featureOverride);
    }
}