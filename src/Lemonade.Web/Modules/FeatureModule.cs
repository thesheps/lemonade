using System.Linq;
using Lemonade.Data.Commands;
using Lemonade.Data.Queries;
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
        public FeatureModule(IGetAllApplications getAllApplications, IGetAllFeaturesByApplicationId getAllFeaturesByApplicationId, IGetFeatureByNameAndApplication getFeatureByNameAndApplication, ISaveFeature saveFeature)
        {
            _getAllApplications = getAllApplications;
            _getAllFeaturesByApplicationId = getAllFeaturesByApplicationId;
            _getFeatureByNameAndApplication = getFeatureByNameAndApplication;
            _saveFeature = saveFeature;

            Get["/api/feature"] = p => GetFeature();
            Post["/api/feature"] = p => PostFeature();

            Get["/feature"] = p => GetAllFeatures();
            Post["/feature"] = p => PostFeatureFromFormData();
        }

        private Feature GetFeature()
        {
            var featureName = Request.Query["feature"].Value as string;
            var applicationName = Request.Query["application"].Value as string;
            var feature = _getFeatureByNameAndApplication.Execute(featureName, applicationName);

            return feature?.ToContract();
        }

        private Negotiator GetAllFeatures()
        {
            int applicationId;
            int.TryParse(Request.Query["applicationId"].Value as string, out applicationId);

            var features = _getAllFeaturesByApplicationId.Execute(applicationId).Select(f => f.ToModel()).ToList();
            var applications = _getAllApplications.Execute().Select(a => a.ToModel()).ToList();
            var indexModel = new IndexModel
            {
                Applications = applications,
                Features = features,
                ApplicationId = applicationId
            };

            return View["Features", indexModel];
        }

        private HttpStatusCode PostFeature()
        {
            _saveFeature.Execute(this.Bind<Feature>().ToEntity());
            return HttpStatusCode.OK;
        }

        private Response PostFeatureFromFormData()
        {
            var feature = this.Bind<FeatureModel>().ToEntity();
            _saveFeature.Execute(feature);
            return Response.AsRedirect($"/feature?applicationId={feature.ApplicationId}");
        }

        private readonly IGetAllApplications _getAllApplications;
        private readonly IGetAllFeaturesByApplicationId _getAllFeaturesByApplicationId;
        private readonly IGetFeatureByNameAndApplication _getFeatureByNameAndApplication;
        private readonly ISaveFeature _saveFeature;
    }
}