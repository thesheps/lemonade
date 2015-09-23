using System.Linq;
using Lemonade.Core.Commands;
using Lemonade.Core.Exceptions;
using Lemonade.Core.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Mappers;
using Lemonade.Web.Models;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;

namespace Lemonade.Web.Modules
{
    public class FeatureModule : NancyModule
    {
        public FeatureModule(IGetAllApplications getAllApplications, IGetAllFeaturesByApplicationId getAllFeaturesByApplicationId, 
            IGetFeatureByNameAndApplication getFeatureByNameAndApplication, ISaveFeature saveFeature)
        {
            _getAllApplications = getAllApplications;
            _getAllFeaturesByApplicationId = getAllFeaturesByApplicationId;
            _getFeatureByNameAndApplication = getFeatureByNameAndApplication;
            _saveFeature = saveFeature;

            Get["/api/feature"] = p => GetFeature();
            Get["/feature"] = p => GetAllFeatures();

            Post["/api/feature"] = p => PostFeature();
            Post["/feature"] = p => PostFeatureFromFormData();
        }

        private Negotiator GetAllFeatures()
        {
            return View["Features", GetFeaturesModel()];
        }

        private HttpStatusCode PostFeature()
        {
            try
            {
                _saveFeature.Execute(this.Bind<Feature>().ToEntity());
            }
            catch (SaveFeatureException exception)
            {
                ModelValidationResult.Errors.Add("SaveException", exception.Message);
            }

            return HttpStatusCode.OK;
        }

        private dynamic PostFeatureFromFormData()
        {
            var feature = this.Bind<FeatureModel>().ToEntity();

            try
            {
                _saveFeature.Execute(feature);
            }
            catch (SaveFeatureException exception)
            {
                ModelValidationResult.Errors.Add("SaveException", exception.Message);
                return View["/features", GetFeaturesModel()];
            }

            return Response.AsRedirect($"/feature?applicationId={feature.ApplicationId}");
        }

        private Feature GetFeature()
        {
            var featureName = Request.Query["feature"].Value as string;
            var applicationName = Request.Query["application"].Value as string;
            var feature = _getFeatureByNameAndApplication.Execute(featureName, applicationName);

            return feature?.ToContract();
        }

        private FeaturesModel GetFeaturesModel()
        {
            int applicationId;
            int.TryParse(Request.Query["applicationId"].Value as string, out applicationId);

            var features = _getAllFeaturesByApplicationId.Execute(applicationId).Select(f => f.ToModel()).ToList();
            var applications = _getAllApplications.Execute().Select(a => a.ToModel()).ToList();

            return new FeaturesModel(applicationId, applications, features);
        }

        private readonly IGetAllApplications _getAllApplications;
        private readonly IGetAllFeaturesByApplicationId _getAllFeaturesByApplicationId;
        private readonly IGetFeatureByNameAndApplication _getFeatureByNameAndApplication;
        private readonly ISaveFeature _saveFeature;
    }
}