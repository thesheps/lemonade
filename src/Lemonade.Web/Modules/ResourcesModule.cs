using System.Collections.Generic;
using System.Linq;
using Lemonade.Data.Exceptions;
using Lemonade.Data.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Core.Commands;
using Lemonade.Web.Core.Mappers;
using Lemonade.Web.Core.Services;
using Nancy;
using Nancy.ModelBinding;

namespace Lemonade.Web.Modules
{
    public class ResourcesModule : NancyModule
    {
        public ResourcesModule(ICommandDispatcher commandDispatcher, IGetResource getResource, IGetAllResourcesByApplicationId getAllResourcesbyApplicationId)
        {
            _commandDispatcher = commandDispatcher;
            _getResource = getResource;
            _getAllResourcesbyApplicationId = getAllResourcesbyApplicationId;
            Get["/api/resource"] = r => GetResource();
            Get["/api/resources"] = r => GetResources();
            Post["/api/resources"] = r => CreateResource();
            Put["/api/resources"] = r => UpdateResource();
            Delete["api/resources"] = r => DeleteResource();
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

        private IList<Resource> GetResources()
        {
            int applicationId;
            int.TryParse(Request.Query["applicationId"].Value as string, out applicationId);

            var resources = _getAllResourcesbyApplicationId.Execute(applicationId);

            return resources.Select(r => r.ToContract()).ToList();
        }

        private HttpStatusCode CreateResource()
        {
            try
            {
                var resource = this.Bind<Resource>();
                _commandDispatcher.Dispatch(new CreateResourceCommand(resource.ApplicationId, resource.Locale, resource.ResourceKey, resource.ResourceSet, resource.Value));

                return HttpStatusCode.OK;
            }
            catch (CreateResourceException)
            {
                return HttpStatusCode.BadRequest;
            }
        }

        private HttpStatusCode UpdateResource()
        {
            try
            {
                var resource = this.Bind<Resource>();
                _commandDispatcher.Dispatch(new UpdateResourceCommand(resource.ResourceId, resource.ResourceSet, resource.ResourceKey, resource.Locale, resource.Value));

                return HttpStatusCode.OK;
            }
            catch (UpdateFeatureException)
            {
                return HttpStatusCode.BadRequest;
            }
        }

        private HttpStatusCode DeleteResource()
        {
            int resourceId;
            int.TryParse(Request.Query["id"].Value as string, out resourceId);

            try
            {
                _commandDispatcher.Dispatch(new DeleteResourceCommand(resourceId));
                return HttpStatusCode.OK;
            }
            catch (DeleteResourceException)
            {
                return HttpStatusCode.BadRequest;
            }
        }

        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IGetResource _getResource;
        private readonly IGetAllResourcesByApplicationId _getAllResourcesbyApplicationId;
    }
}