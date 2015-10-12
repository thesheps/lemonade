using Lemonade.Web.Contracts;

namespace Lemonade.Web.Mappers
{
    public static class ApplicationMapper
    {
        public static Application ToContract(this Data.Entities.Application application)
        {
            return new Application
            {
                ApplicationId = application.ApplicationId,
                Name = application.Name
            };
        }

        public static Data.Entities.Application ToDomain(this Application application)
        {
            return new Data.Entities.Application
            {
                ApplicationId = application.ApplicationId,
                Name = application.Name
            };
        }
    }
}