using System.Linq;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Mappers;
using Nancy;
using Nancy.ModelBinding;

namespace Lemonade.Web.Modules
{
    public class FeatureModule : NancyModule
    {
        public FeatureModule(GetAllFeatures getAllFeatures, GetFeatureByNameAndApplication getFeatureByNameAndApplication, SaveFeature saveFeature)
        {
            Get["/features"] = parameters => new FeaturesModel { Features = getAllFeatures.Execute().Select(f => f.ToModel()).ToList() };

            Get["/api/features"] = parameters => getAllFeatures.Execute().Select(f => f.ToModel()).ToList();

            Get["/api/feature"] = parameters =>
            {
                var feature = Request.Query["feature"].Value as string;
                var application = Request.Query["application"].Value as string;
                var model = getFeatureByNameAndApplication.Execute(feature, application).ToModel();
                return model;
            };

            Post["/api/feature"] = parameters =>
            {
                saveFeature.Execute(this.Bind<FeatureModel>().ToEntity());
                return HttpStatusCode.OK;
            };
        }
    }
}