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
        public FeatureModule(IGetAllFeatures getAllFeatures, IGetFeatureByNameAndApplication getFeatureByNameAndApplication, ISaveFeature saveFeature)
        {
            Get["/features"] = parameters => View["Features", getAllFeatures.Execute().Select(f => f.ToModel()).ToList()];

            Get["/api/features"] = parameters => getAllFeatures.Execute().Select(f => f.ToContract()).ToList();

            Get["/api/feature"] = parameters =>
            {
                var featureName = Request.Query["feature"].Value as string;
                var applicationName = Request.Query["application"].Value as string;
                var feature = getFeatureByNameAndApplication.Execute(featureName, applicationName);

                return feature?.ToContract();
            };

            Post["/api/feature"] = parameters =>
            {
                saveFeature.Execute(this.Bind<Feature>().ToEntity());
                return HttpStatusCode.OK;
            };
        }
    }
}