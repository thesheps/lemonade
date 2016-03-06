using Lemonade.Data.Entities;

namespace Lemonade.Web.Core.Mappers
{
    public static class ConfigurationMapper
    {
        public static Contracts.Configuration ToContract(this Configuration configuration)
        {
            return new Contracts.Configuration
            {
                ConfigurationId = configuration.ConfigurationId,
                Name = configuration.Name,
                Application = configuration.Application.ToContract(),
                Value = configuration.Value
            };
        }

        public static Configuration ToEntity(this Contracts.Configuration configuration)
        {
            return new Configuration
            {
                ConfigurationId = configuration.ConfigurationId,
                ApplicationId = configuration.ApplicationId,
                Name = configuration.Name,
                Value = configuration.Value
            };
        }
    }
}