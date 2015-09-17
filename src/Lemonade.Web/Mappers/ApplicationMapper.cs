namespace Lemonade.Web.Mappers
{
    public static class ApplicationMapper
    {
        public static Models.ApplicationModel ToModel(this Data.Entities.Application application)
        {
            return new Models.ApplicationModel
            {
                Id = application.Id,
                Name = application.Name
            };
        }
    }
}