using Lemonade.Data.Entities;

namespace Lemonade.Web.Core.Mappers
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

        public static Application ToEntity(this Contracts.Application application)
        {
            return new Application
            {
                ApplicationId = application.ApplicationId,
                Name = application.Name
            };
        }
    }
}