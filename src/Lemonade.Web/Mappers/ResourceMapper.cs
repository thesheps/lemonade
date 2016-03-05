using Lemonade.Web.Contracts;

namespace Lemonade.Web.Mappers
{
    public static class ResourceMapper
    {
        public static Resource ToContract(this Data.Entities.Resource resource)
        {
            return new Resource
            {
                ApplicationId = resource.ApplicationId,
                Locale = resource.Locale,
                ResourceKey = resource.ResourceKey,
                ResourceId = resource.ResourceId,
                ResourceSet = resource.ResourceSet,
                Value = resource.Value,
                Application = resource.Application.ToContract()
            };
        }

        public static Data.Entities.Resource ToEntity(this Resource resource)
        {
            return new Data.Entities.Resource
            {
                ApplicationId = resource.ApplicationId,
                Locale = resource.Locale,
                ResourceKey = resource.ResourceKey,
                ResourceId = resource.ResourceId,
                ResourceSet = resource.ResourceSet,
                Value = resource.Value,
                Application = resource.Application.ToEntity()
            };
        }
    }
}