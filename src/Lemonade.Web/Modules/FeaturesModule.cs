using System.Collections.Generic;
using System.Linq;
using Lemonade.Core.Commands;
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
        public FeaturesModule(IGetFeatureByNameAndApplication getFeatureByNameAndApplication, IGetAllFeaturesByApplicationId getAllFeaturesByApplicationId, ISaveFeature saveFeature)
        {
            _getFeatureByNameAndApplication = getFeatureByNameAndApplication;
            _getAllFeaturesByApplicationId = getAllFeaturesByApplicationId;
            _saveFeature = saveFeature;

            Post["/api/features"] = p => PostFeature();
            Get["/api/features"] = p => GetFeatures();
            Get["/api/feature"] = p => GetFeature();
            Get["/features"] = p => View["Features"];
        }

        private HttpStatusCode PostFeature()
        {
            try
            {
                _saveFeature.Execute(this.Bind<Feature>().ToDomain());
            }
            catch (SaveFeatureException exception)
            {
                ModelValidationResult.Errors.Add("SaveException", exception.Message);
            }

            return HttpStatusCode.OK;
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

        private readonly IGetFeatureByNameAndApplication _getFeatureByNameAndApplication;
        private readonly IGetAllFeaturesByApplicationId _getAllFeaturesByApplicationId;
        private readonly ISaveFeature _saveFeature;
    }
}