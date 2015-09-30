using System.Collections.Generic;
using System.Linq;
using Lemonade.Core.Commands;
using Lemonade.Core.Events;
using Lemonade.Core.Exceptions;
using Lemonade.Core.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Mappers;
using Nancy;
using Nancy.ModelBinding;

namespace Lemonade.Web.Modules
{
    public class FeaturesModule : NancyModule
    {
        public FeaturesModule(IGetFeatureByNameAndApplication getFeatureByNameAndApplication, IGetAllFeaturesByApplicationId getAllFeaturesByApplicationId, ISaveFeature saveFeature, IDeleteFeature deleteFeature)
        {
            _getFeatureByNameAndApplication = getFeatureByNameAndApplication;
            _getAllFeaturesByApplicationId = getAllFeaturesByApplicationId;
            _saveFeature = saveFeature;
            _deleteFeature = deleteFeature;

            Post["/api/features"] = p => PostFeature();
            Get["/api/features"] = p => GetFeatures();
            Get["/api/feature"] = p => GetFeature();
            Get["/features"] = p => View["Features"];
            Delete["/api/features"] = p => DeleteFeature();
        }

        private HttpStatusCode PostFeature()
        {
            try
            {
                _saveFeature.Execute(this.Bind<Feature>().ToDomain());
                return HttpStatusCode.OK;
            }
            catch (SaveFeatureException exception)
            {
                DomainEvent.Raise(new ErrorHasOccurred(exception.Message));
                return HttpStatusCode.BadRequest;
            }
        }

        private Feature GetFeature()
        {
            var featureName = Request.Query["feature"].Value as string;
            var applicationName = Request.Query["application"].Value as string;
            var feature = _getFeatureByNameAndApplication.Execute(featureName, applicationName);

            return feature?.ToContract();
        }

        private IList<Feature> GetFeatures()
        {
            int applicationId;
            int.TryParse(Request.Query["applicationId"].Value as string, out applicationId);

            var feature = _getAllFeaturesByApplicationId.Execute(applicationId);

            return feature.Select(f => f.ToContract()).ToList();
        }

        private dynamic DeleteFeature()
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

        private readonly IGetFeatureByNameAndApplication _getFeatureByNameAndApplication;
        private readonly IGetAllFeaturesByApplicationId _getAllFeaturesByApplicationId;
        private readonly ISaveFeature _saveFeature;
        private readonly IDeleteFeature _deleteFeature;
    }
}