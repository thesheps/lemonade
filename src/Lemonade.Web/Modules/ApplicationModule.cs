using System.Linq;
using Lemonade.Data.Commands;
using Lemonade.Data.Exceptions;
using Lemonade.Data.Queries;
using Lemonade.Web.Mappers;
using Lemonade.Web.Models;
using Nancy;
using Nancy.ModelBinding;

namespace Lemonade.Web.Modules
{
    public class ApplicationModule : NancyModule
    {
        public ApplicationModule(IGetAllApplications getAllApplications, IGetAllFeaturesByApplicationId getAllFeaturesByApplicationId, ISaveApplication saveApplication)
        {
            _getAllApplications = getAllApplications;
            _getAllFeaturesByApplicationId = getAllFeaturesByApplicationId;
            _saveApplication = saveApplication;

            Post["/application"] = p => PostApplication();
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
                return View["/features", GetFeaturesModel()];
            }

            return Response.AsRedirect("/feature");
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
    }
}