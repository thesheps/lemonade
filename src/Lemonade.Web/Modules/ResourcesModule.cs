using Lemonade.Data.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Mappers;
using Nancy;

namespace Lemonade.Web.Modules
{
    public class ResourcesModule : NancyModule
    {
        public ResourcesModule(IGetResource getResource)
        {
            _getResource = getResource;
            Get["/api/resource"] = p => GetResource();
        }

        private Resource GetResource()
        {
            var application = Request.Query["application"].Value as string;
            var resourceSet = Request.Query["resourceSet"].Value as string;
            var resourceKey = Request.Query["resourceKey"].Value as string;
            var locale = Request.Query["locale"].Value as string;
            var resource = _getResource.Execute(application, resourceSet, resourceKey, locale);

            return resource.ToContract();
        }

        private readonly IGetResource _getResource;
    }
}