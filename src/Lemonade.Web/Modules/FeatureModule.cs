using System.Linq;
using Lemonade.Data.Commands;
using Lemonade.Data.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Mappers;
using Nancy;
using Nancy.ModelBinding;

namespace Lemonade.Web.Modules
{
    public class FeatureModule : NancyModule
    {
        public FeatureModule(IGetAllFeaturesByApplication getAllFeaturesByApplication, IGetFeatureByNameAndApplication getFeatureByNameAndApplication, ISaveFeature saveFeature)
        {
            Get["/"] = p => View["Index"];

            Get["/features"] = p =>
            {
                var applicationName = Request.Query["application"].Value as string;
                return View["Features", getAllFeaturesByApplication.Execute(applicationName).Select(f => f.ToModel()).ToList()];
            };

            Get["/api/features"] = p =>
            {
                var applicationName = Request.Query["application"].Value as string;
                return getAllFeaturesByApplication.Execute(applicationName).Select(f => f.ToContract()).ToList();
            };

            Get["/api/feature"] = p =>
            {
                var featureName = Request.Query["feature"].Value as string;
                var applicationName = Request.Query["application"].Value as string;
                var feature = getFeatureByNameAndApplication.Execute(featureName, applicationName);

                return feature?.ToContract();
            };

            Post["/api/feature"] = p =>
            {
                saveFeature.Execute(this.Bind<Feature>().ToEntity());
                return HttpStatusCode.OK;
            };
        }
    }
}