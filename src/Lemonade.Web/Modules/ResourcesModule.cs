﻿using Lemonade.Data.Commands;
using Lemonade.Data.Exceptions;
using Lemonade.Data.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Events;
using Lemonade.Web.Mappers;
using Nancy;
using Nancy.ModelBinding;

namespace Lemonade.Web.Modules
{
    public class ResourcesModule : NancyModule
    {
        public ResourcesModule(IGetResource getResource, ICreateResource createResource)
        {
            _getResource = getResource;
            _createResource = createResource;
            Get["/api/resource"] = p => GetResource();
            Post["/api/resources"] = p => CreateResource();
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

        private HttpStatusCode CreateResource()
        {
            try
            {
                var resource = this.Bind<Resource>().ToEntity();
                _createResource.Execute(resource);
                DomainEvents.Raise(new ResourceHasBeenCreated(resource.ResourceId, resource.ApplicationId, resource.ResourceSet, resource.ResourceKey, resource.Locale, resource.Value));

                return HttpStatusCode.OK;
            }
            catch (CreateResourceException exception)
            {
                DomainEvents.Raise(new ResourceErrorHasOccurred(exception.Message));
                return HttpStatusCode.BadRequest;
            }
        }

        private readonly IGetResource _getResource;
        private readonly ICreateResource _createResource;
    }
}