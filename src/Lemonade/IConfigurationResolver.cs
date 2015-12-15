namespace Lemonade
{
    public interface IConfigurationResolver
    {
        T Resolve<T>(string key, string applicationName);
    }
}