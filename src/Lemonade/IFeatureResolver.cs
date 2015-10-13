namespace Lemonade
{
    public interface IFeatureResolver
    {
        bool Resolve(string featureName, string applicationName);
    }
}