﻿using System;
using System.Collections.Generic;
using Lemonade.Web.Contracts;
using Lemonade.Web.Core.Commands;
using Lemonade.Web.Core.Queries;
using Lemonade.Web.Core.Services;
using Lemonade.Web.Infrastructure;
using Nancy;
using Nancy.ModelBinding;

namespace Lemonade.Web.Modules
{
    public class ResourcesModule : NancyModule
    {
        public ResourcesModule(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            Get["/api/resource"] = r => GetResource();
            Get["/api/resources"] = r => GetResources();
            Post["/api/resources"] = r => CreateResource();
            Post["/api/resources/generate"] = r => GenerateResources();
            Put["/api/resources"] = r => UpdateResource();
            Delete["api/resources"] = r => DeleteResource();
        }

        private Resource GetResource()
        {
            var application = Request.Query["application"].Value as string;
            var resourceSet = Request.Query["resourceSet"].Value as string;
            var resourceKey = Request.Query["resourceKey"].Value as string;
            var locale = Request.Query["locale"].Value as string;
            var resource = _queryDispatcher.Dispatch(new GetResourceQuery(application, resourceSet, resourceKey, locale));

            return resource;
        }

        private IList<Resource> GetResources()
        {
            int applicationId;
            int.TryParse(Request.Query["applicationId"].Value as string, out applicationId);

            var resources = _queryDispatcher.Dispatch(new GetAllResourcesByApplicationIdQuery(applicationId));

            return resources;
        }

        private Response CreateResource()
        {
            try
            {
                var resource = this.Bind<Resource>();
                _commandDispatcher.Dispatch(new CreateResourceCommand(resource.ApplicationId, resource.LocaleId, resource.ResourceKey, resource.ResourceSet, resource.Value));

                return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                return Responses.BadRequest(ex);
            }
        }

        private Response GenerateResources()
        {
            try
            {
                var generateResources = this.Bind<GenerateResources>();
                _commandDispatcher.Dispatch(new GenerateResourcesCommand(generateResources.ApplicationId, generateResources.LocaleId, generateResources.TargetLocaleId, generateResources.TranslationType));

                return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                return Responses.BadRequest(ex);
            }
        }

        private Response UpdateResource()
        {
            try
            {
                var resource = this.Bind<Resource>();
                _commandDispatcher.Dispatch(new UpdateResourceCommand(resource.ResourceId, resource.LocaleId, resource.ResourceKey, resource.ResourceSet, resource.Value));

                return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                return Responses.BadRequest(ex);
            }
        }

        private Response DeleteResource()
        {
            int resourceId;
            int.TryParse(Request.Query["id"].Value as string, out resourceId);

            try
            {
                _commandDispatcher.Dispatch(new DeleteResourceCommand(resourceId));
                return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                return Responses.BadRequest(ex);
            }
        }

        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
    }
}