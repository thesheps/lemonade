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
        public FeatureModule(IGetAllApplications getAllApplications, IGetAllFeaturesByApplication getAllFeaturesByApplication, IGetFeatureByNameAndApplication getFeatureByNameAndApplication, ISaveFeature saveFeature)
        {
            _getAllApplications = getAllApplications;
            _getAllFeaturesByApplication = getAllFeaturesByApplication;
            _getFeatureByNameAndApplication = getFeatureByNameAndApplication;
            _saveFeature = saveFeature;

            Get["/feature"] = p => GetAllFeatures();
            Get["/api/feature"] = p => GetFeature();
            Post["/api/feature"] = p => PostFeature();
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
            var applicationName = Request.Query["application"].Value as string;
            var features = _getAllFeaturesByApplication.Execute(applicationName).Select(f => f.ToModel()).ToList();
            var applications = _getAllApplications.Execute().Select(a => a.ToModel()).ToList();
            var indexModel = new IndexModel { Applications = applications, Features = features };

            return View["Features", indexModel];
        }

        private HttpStatusCode PostFeature()
        {
            _saveFeature.Execute(this.Bind<Feature>().ToEntity());
            return HttpStatusCode.OK;
        }

        private readonly IGetAllApplications _getAllApplications;
        private readonly IGetAllFeaturesByApplication _getAllFeaturesByApplication;
        private readonly IGetFeatureByNameAndApplication _getFeatureByNameAndApplication;
        private readonly ISaveFeature _saveFeature;
    }
}