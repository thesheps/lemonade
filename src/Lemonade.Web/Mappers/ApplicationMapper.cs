namespace Lemonade.Web.Mappers
{
    public static class ApplicationMapper
    {
        public static Models.ApplicationModel ToModel(this Data.Entities.Application application)
        {
            return new Models.ApplicationModel
            {
                Id = application.ApplicationId,
                Name = application.Name
            };
        }

        public static Contracts.Application ToContract(this Data.Entities.Application application)
        {
            return new Contracts.Application
            {
                Id = application.ApplicationId,
                Name = application.Name
            };
        }

        public static Data.Entities.Application ToEntity(this Contracts.Application application)
        {
            return new Data.Entities.Application
            {
                ApplicationId = application.Id,
                Name = application.Name
            };
        }

        public static Data.Entities.Application ToEntity(this Models.ApplicationModel application)
        {
            return new Data.Entities.Application
            {
                ApplicationId = application.Id,
                Name = application.Name
            };
        }
    }
}