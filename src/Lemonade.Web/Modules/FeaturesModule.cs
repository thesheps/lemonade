using System.Collections.Generic;
using System.Linq;
using Lemonade.Data.Commands;
using Lemonade.Data.Exceptions;
using Lemonade.Data.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Events;
using Lemonade.Web.Mappers;
using Nancy;
using Nancy.ModelBinding;

namespace Lemonade.Web.Modules
{
    public class FeaturesModule : NancyModule
    {
        public FeaturesModule(IGetFeatureByNameAndApplication getFeatureByNameAndApplication, IGetApplicationByName getApplicationByName, IGetAllFeaturesByApplicationId getAllFeaturesByApplicationId,
            ICreateFeature createFeature, IUpdateFeature updateFeature, IDeleteFeature deleteFeature, ICreateApplication createApplication)
        {
            _getFeatureByNameAndApplication = getFeatureByNameAndApplication;
            _getApplicationByName = getApplicationByName;
            _getAllFeaturesByApplicationId = getAllFeaturesByApplicationId;
            _createFeature = createFeature;
            _updateFeature = updateFeature;
            _deleteFeature = deleteFeature;
            _createApplication = createApplication;

            Post["/api/features"] = p => CreateFeature();
            Put["/api/features"] = p => UpdateFeature();
            Get["/api/features"] = p => GetFeatures();
            Get["/api/feature"] = p => GetFeature();
            Get["/features"] = p => View["Features"];
            Delete["/api/features"] = p => DeleteFeature();
        }

        private Feature GetFeature()
        {
            var featureName = Request.Query["feature"].Value as string;
            var applicationName = Request.Query["application"].Value as string;
            var feature = _getFeatureByNameAndApplication.Execute(featureName, applicationName);

            if (feature != null) return feature.ToContract();

            var application = GetApplication(applicationName);
            feature = new Data.Entities.Feature { Name = featureName, ApplicationId = application.ApplicationId, Application = application };
            _createFeature.Execute(feature);
            DomainEvent.Raise(new FeatureHasBeenCreated(feature.FeatureId, feature.ApplicationId, feature.Name, feature.StartDate, feature.ExpirationDays, feature.IsEnabled));

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
                var feature = this.Bind<Feature>().ToDomain();
                _createFeature.Execute(feature);
                DomainEvent.Raise(new FeatureHasBeenCreated(feature.FeatureId, feature.ApplicationId, feature.Name, feature.StartDate, feature.ExpirationDays, feature.IsEnabled));

                return HttpStatusCode.OK;
            }
            catch (CreateFeatureException exception)
            {
                DomainEvent.Raise(new ErrorHasOccurred(exception.Message));
                return HttpStatusCode.BadRequest;
            }
        }

        private HttpStatusCode UpdateFeature()
        {
            try
            {
                _updateFeature.Execute(this.Bind<Feature>().ToDomain());
                return HttpStatusCode.OK;
            }
            catch (CreateFeatureException exception)
            {
                DomainEvent.Raise(new ErrorHasOccurred(exception.Message));
                return HttpStatusCode.BadRequest;
            }
        }

        private HttpStatusCode DeleteFeature()
        {
            int featureId;
            int.TryParse(Request.Query["id"].Value as string, out featureId);

            try
            {
                _deleteFeature.Execute(featureId);
                return HttpStatusCode.OK;
            }
            catch (DeleteFeatureException exception)
            {
                DomainEvent.Raise(new ErrorHasOccurred(exception.Message));
                return HttpStatusCode.BadRequest;
            }
        }

        private Data.Entities.Application GetApplication(string applicationName)
        {
            var application = _getApplicationByName.Execute(applicationName);
            if (application != null) return application;

            application = new Data.Entities.Application { Name = applicationName };
            _createApplication.Execute(application);

            return application;
        }

        private readonly IGetFeatureByNameAndApplication _getFeatureByNameAndApplication;
        private readonly IGetApplicationByName _getApplicationByName;
        private readonly IGetAllFeaturesByApplicationId _getAllFeaturesByApplicationId;
        private readonly ICreateFeature _createFeature;
        private readonly IUpdateFeature _updateFeature;
        private readonly IDeleteFeature _deleteFeature;
        private readonly ICreateApplication _createApplication;
    }
}