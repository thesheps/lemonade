using System.Linq;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Queries;
using Lemonade.Web.Mappers;
using Lemonade.Web.Models;
using Nancy;
using Nancy.ModelBinding;

namespace Lemonade.Web.Modules
{
    public class FeatureModule : NancyModule
    {
        public FeatureModule(GetAllFeatures getAllFeatures, SaveFeature saveFeature)
        {
            Get["/feature"] = parameters => new FeaturesModel { Features = getAllFeatures.Execute().Select(f => f.ToModel()).ToList() };
            Get["/api/feature"] = parameters => getAllFeatures.Execute().Select(f => f.ToModel()).ToList();
            Post["/api/feature"] = parameters =>
            {
                saveFeature.Execute(this.Bind<FeatureModel>().ToEntity());
                return HttpStatusCode.OK;
            };
        }
    }
}