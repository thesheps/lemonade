using System.Linq;
using Lemonade.Web.Mappers;
using Lemonade.Web.Models;
using Nancy;
using Nancy.ModelBinding;
using System.Collections.Generic;
using Lemonade.Core.Commands;
using Lemonade.Core.Exceptions;
using Lemonade.Core.Queries;

namespace Lemonade.Web.Modules
{
    public class ApplicationModule : NancyModule
    {
        public ApplicationModule(IGetAllApplications getAllApplications, IGetAllFeaturesByApplicationId getAllFeaturesByApplicationId, ISaveApplication saveApplication, IDeleteApplication deleteApplication)
        {
            _getAllApplications = getAllApplications;
            _getAllFeaturesByApplicationId = getAllFeaturesByApplicationId;
            _saveApplication = saveApplication;
            _deleteApplication = deleteApplication;

            Get["/api/application"] = p => GetApplications();

            Post["/application"] = p => PostApplicationFromFormData();
            Post["/api/application"] = p => PostApplication();

            Delete["/api/application"] = p => DeleteApplication();
        }

        private IList<Contracts.Application> GetApplications()
        {
            return _getAllApplications.Execute().Select(a => a.ToContract()).ToList();
        }

        private dynamic PostApplication()
        {
            try
            {
                _saveApplication.Execute(this.Bind<ApplicationModel>().ToDomain());
                return HttpStatusCode.OK;
            }
            catch (SaveApplicationException exception)
            {
                ModelValidationResult.Errors.Add("SaveException", exception.Message);
                return HttpStatusCode.BadRequest;
            }
        }

        private dynamic PostApplicationFromFormData()
        {
            try
            {
                _saveApplication.Execute(this.Bind<ApplicationModel>().ToDomain());
            }
            catch (SaveApplicationException exception)
            {
                ModelValidationResult.Errors.Add("SaveException", exception.Message);
                return View["/features", GetFeaturesModel()];
            }

            return Response.AsRedirect("/feature");
        }

        private dynamic DeleteApplication()
        {
            int applicationId;
            int.TryParse(Request.Query["id"].Value as string, out applicationId);

            try
            {
                _deleteApplication.Execute(applicationId);
                return HttpStatusCode.OK;
            }
            catch (DeleteApplicationException exception)
            {
                ModelValidationResult.Errors.Add("DeleteException", exception.Message);
                return HttpStatusCode.BadRequest;
            }
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
        private readonly ISaveApplication _saveApplication;
        private readonly IDeleteApplication _deleteApplication;
    }
}