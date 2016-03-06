using Lemonade.Web.Contracts;

namespace Lemonade.Web.Core.Queries
{
    public class GetConfigurationByNameAndApplicationQuery : IQuery<Configuration>
    {
        public string ApplicationName { get; }
        public string ConfigurationName { get; }

        public GetConfigurationByNameAndApplicationQuery(string applicationName, string configurationName)
        {
            ApplicationName = applicationName;
            ConfigurationName = configurationName;
        }
    }
}