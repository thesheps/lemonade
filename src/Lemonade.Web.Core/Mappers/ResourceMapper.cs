using Lemonade.Data.Entities;

namespace Lemonade.Web.Core.Mappers
{
    public static class ResourceMapper
    {
        public static Contracts.Resource ToContract(this Resource resource)
        {
            return new Contracts.Resource
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

        public static Resource ToEntity(this Contracts.Resource resource)
        {
            return new Resource
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