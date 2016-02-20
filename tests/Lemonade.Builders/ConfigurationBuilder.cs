using Lemonade.Data.Entities;

namespace Lemonade.Builders
{
    public class ConfigurationBuilder
    {
        public ConfigurationBuilder WithName(string name)
        {
            _configuration.Name = name;
            return this;
        }

        public ConfigurationBuilder WithApplication(Application application)
        {
            _configuration.Application = application;
            _configuration.ApplicationId = application.ApplicationId;
            return this;
        }

        public ConfigurationBuilder WithValue(string value)
        {
            _configuration.Value = value;
            return this;
        }

        public Configuration Build()
        {
            return _configuration;
        }

        private readonly Configuration _configuration = new Configuration();
    }
}