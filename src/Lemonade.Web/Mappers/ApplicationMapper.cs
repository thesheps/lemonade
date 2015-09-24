using Lemonade.Core.Domain;

namespace Lemonade.Web.Mappers
{
    public static class ApplicationMapper
    {
        public static Models.ApplicationModel ToModel(this Application application)
        {
            return new Models.ApplicationModel
            {
                Id = application.ApplicationId,
                Name = application.Name
            };
        }

        public static Contracts.Application ToContract(this Application application)
        {
            return new Contracts.Application
            {
                Id = application.ApplicationId,
                Name = application.Name
            };
        }

        public static Application ToDomain(this Contracts.Application application)
        {
            return new Application
            {
                ApplicationId = application.Id,
                Name = application.Name
            };
        }

        public static Application ToDomain(this Models.ApplicationModel application)
        {
            return new Application
            {
                ApplicationId = application.Id,
                Name = application.Name
            };
        }
    }
}