namespace Lemonade.Services
{
    public interface IFeatureResolver
    {
        bool? Get(string value);
    }
}