using Lemonade.Core.Domain;

namespace Lemonade.Web.Mappers
{
    public static class ApplicationMapper
    {
        public static Contracts.Application ToContract(this Application application)
        {
            return new Contracts.Application
            {
                ApplicationId = application.ApplicationId,
                Name = application.Name
            };
        }

        public static Application ToDomain(this Contracts.Application application)
        {
            return new Application
            {
                ApplicationId = application.ApplicationId,
                Name = application.Name
            };
        }
    }
}