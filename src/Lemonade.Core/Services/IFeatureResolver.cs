namespace Lemonade.Core.Services
{
    public interface IFeatureResolver
    {
        bool Resolve(string featureName, string applicationName);
    }
}