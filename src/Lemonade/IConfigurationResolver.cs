namespace Lemonade
{
    public interface IConfigurationResolver
    {
        T Resolve<T>(string configurationName, string applicationName);
    }
}