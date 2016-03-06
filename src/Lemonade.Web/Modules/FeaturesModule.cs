using System;
using System.Collections.Generic;
using Lemonade.Web.Contracts;
using Lemonade.Web.Core.Commands;
using Lemonade.Web.Core.Queries;
using Lemonade.Web.Core.Services;
using Nancy;
using Nancy.ModelBinding;

namespace Lemonade.Web.Modules
{
    public class FeaturesModule : NancyModule
    {
        public FeaturesModule(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;

            Post["/api/features"] = p => CreateFeature();
            Put["/api/features"] = p => UpdateFeature();
            Get["/api/features"] = p => GetFeatures();
            Get["/api/feature"] = p => GetFeature();
            Delete["/api/features"] = p => DeleteFeature();
        }

        private Feature GetFeature()
        {
            var featureName = Request.Query["feature"].Value as string;
            var applicationName = Request.Query["application"].Value as string;
            var feature = _queryDispatcher.Dispatch(new GetFeatureByNameAndApplicationQuery(applicationName, featureName));

            return feature;
        }

        private IList<Feature> GetFeatures()
        {
            int applicationId;
            int.TryParse(Request.Query["applicationId"].Value as string, out applicationId);

            var features = _queryDispatcher.Dispatch(new GetAllFeaturesByApplicationIdQuery(applicationId));

            return features;
        }

        private HttpStatusCode CreateFeature()
        {
            try
            {
                var feature = this.Bind<Feature>();
                _commandDispatcher.Dispatch(new CreateFeatureCommand(feature.Name, feature.ApplicationId, feature.IsEnabled));

                return HttpStatusCode.OK;
            }
            catch (Exception)
            {
                return HttpStatusCode.BadRequest;
            }
        }

        private HttpStatusCode UpdateFeature()
        {
            try
            {
                var feature = this.Bind<Feature>();
                _commandDispatcher.Dispatch(new UpdateFeatureCommand(feature.FeatureId, feature.Name, feature.IsEnabled));

                return HttpStatusCode.OK;
            }
            catch (Exception)
            {
                return HttpStatusCode.BadRequest;
            }
        }

        private HttpStatusCode DeleteFeature()
        {
            int featureId;
            int.TryParse(Request.Query["id"].Value as string, out featureId);

            try
            {
                _commandDispatcher.Dispatch(new DeleteFeatureCommand(featureId));
                return HttpStatusCode.OK;
            }
            catch (Exception)
            {
                return HttpStatusCode.BadRequest;
            }
        }

        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
    }
}