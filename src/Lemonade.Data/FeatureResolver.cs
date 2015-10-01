namespace Lemonade.Core
{
    public interface IFeatureResolver
    {
        bool Resolve(string featureName);
    }
}