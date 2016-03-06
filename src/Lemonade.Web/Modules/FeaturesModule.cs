using System.Collections.Generic;
using System.Linq;
using Lemonade.Data.Exceptions;
using Lemonade.Data.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Core.Commands;
using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Mappers;
using Lemonade.Web.Core.Services;
using Nancy;
using Nancy.ModelBinding;

namespace Lemonade.Web.Modules
{
    public class FeaturesModule : NancyModule
    {
        public FeaturesModule(IDomainEventDispatcher eventDispatcher, ICommandDispatcher commandDispatcher, IGetAllFeaturesByApplicationId getAllFeaturesByApplicationId, IGetFeatureByNameAndApplication getFeatureByNameAndApplication, IGetApplicationByName getApplicationByName)
        {
            _eventDispatcher = eventDispatcher;
            _commandDispatcher = commandDispatcher;
            _getFeatureByNameAndApplication = getFeatureByNameAndApplication;
            _getApplicationByName = getApplicationByName;
            _getAllFeaturesByApplicationId = getAllFeaturesByApplicationId;

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
            var feature = _getFeatureByNameAndApplication.Execute(featureName, applicationName);
            if (feature != null) return feature.ToContract();
            var application = _getApplicationByName.Execute(applicationName);
            _commandDispatcher.Dispatch(new CreateFeatureCommand(featureName, application.ApplicationId, false));

            feature = _getFeatureByNameAndApplication.Execute(featureName, applicationName);

            return feature.ToContract();
        }

        private IList<Feature> GetFeatures()
        {
            int applicationId;
            int.TryParse(Request.Query["applicationId"].Value as string, out applicationId);

            var feature = _getAllFeaturesByApplicationId.Execute(applicationId);

            return feature.Select(f => f.ToContract()).ToList();
        }

        private HttpStatusCode CreateFeature()
        {
            try
            {
                var feature = this.Bind<Feature>().ToEntity();
                _commandDispatcher.Dispatch(new CreateFeatureCommand(feature.Name, feature.ApplicationId, feature.IsEnabled));

                return HttpStatusCode.OK;
            }
            catch (CreateFeatureException exception)
            {
                _eventDispatcher.Dispatch(new FeatureErrorHasOccurred(exception.Message));
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
            catch (UpdateFeatureException exception)
            {
                _eventDispatcher.Dispatch(new FeatureErrorHasOccurred(exception.Message));
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
            catch (DeleteFeatureException exception)
            {
                _eventDispatcher.Dispatch(new FeatureErrorHasOccurred(exception.Message));
                return HttpStatusCode.BadRequest;
            }
        }

        private readonly IDomainEventDispatcher _eventDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IGetFeatureByNameAndApplication _getFeatureByNameAndApplication;
        private readonly IGetApplicationByName _getApplicationByName;
        private readonly IGetAllFeaturesByApplicationId _getAllFeaturesByApplicationId;
    }
}