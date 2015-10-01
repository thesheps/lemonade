namespace Lemonade.Core
{
    public interface IFeatureResolver
    {
        bool Get(string featureName);
    }
}