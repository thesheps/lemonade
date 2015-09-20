using System.Linq;
using Lemonade.Data.Commands;
using Lemonade.Data.Exceptions;
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
        public FeatureModule(IGetAllApplications getAllApplications, IGetAllFeaturesByApplicationId getAllFeaturesByApplicationId, 
            IGetFeatureByNameAndApplication getFeatureByNameAndApplication, ISaveFeature saveFeature, ISaveApplication saveApplication)
        {
            _getAllApplications = getAllApplications;
            _getAllFeaturesByApplicationId = getAllFeaturesByApplicationId;
            _getFeatureByNameAndApplication = getFeatureByNameAndApplication;
            _saveFeature = saveFeature;
            _saveApplication = saveApplication;

            Get["/api/feature"] = p => GetFeature();
            Get["/feature"] = p => GetAllFeatures();

            Post["/api/feature"] = p => PostFeature();
            Post["/feature"] = p => PostFeatureFromFormData();
            Post["/application"] = p => PostApplication();
        }

        private Negotiator GetAllFeatures()
        {
            return View["Features", GetIndexModel()];
        }

        private dynamic PostApplication()
        {
            try
            {
                _saveApplication.Execute(this.Bind<ApplicationModel>().ToEntity());
            }
            catch (SaveApplicationException exception)
            {
                ModelValidationResult.Errors.Add("SaveException", exception.Message);
                return View["/features", GetIndexModel()];
            }

            return Response.AsRedirect("/feature");
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
                return View["/features", GetIndexModel()];
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

        private IndexModel GetIndexModel()
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

            return indexModel;
        }

        private readonly IGetAllApplications _getAllApplications;
        private readonly IGetAllFeaturesByApplicationId _getAllFeaturesByApplicationId;
        private readonly IGetFeatureByNameAndApplication _getFeatureByNameAndApplication;
        private readonly ISaveFeature _saveFeature;
        private readonly ISaveApplication _saveApplication;
    }
}