using Lemonade.Data.Entities;

namespace Lemonade.Data.Commands
{
    public interface ICreateFeatureOverride
    {
        void Execute(FeatureOverride featureOverride);
    }
}