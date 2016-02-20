using Lemonade.Web.Contracts;

namespace Lemonade.Web.Mappers
{
    public static class ConfigurationMapper
    {
        public static Configuration ToContract(this Data.Entities.Configuration configuration)
        {
            return new Configuration
            {
                ConfigurationId = configuration.ConfigurationId,
                Name = configuration.Name,
                Application = configuration.Application.ToContract(),
                Value = configuration.Value
            };
        }

        public static Data.Entities.Configuration ToEntity(this Configuration configuration)
        {
            return new Data.Entities.Configuration
            {
                ConfigurationId = configuration.ConfigurationId,
                ApplicationId = configuration.ApplicationId,
                Name = configuration.Name,
                Value = configuration.Value
            };
        }
    }
}