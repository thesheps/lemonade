namespace Lemonade.Core.Services
{
    public interface IConfigurationResolver
    {
        T Resolve<T>(string configurationName, string applicationName);
    }
}