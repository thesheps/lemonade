using System.Linq;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Queries;
using Lemonade.Web.Mappers;
using Lemonade.Web.Models;
using Nancy;
using Nancy.ModelBinding;

namespace Lemonade.Web.Modules
{
    public class FeaturesModule : NancyModule
    {
        public FeaturesModule(GetAllFeatures getAllFeatures, SaveFeature saveFeature)
        {
            Get["/features"] = parameters => View[new FeaturesModel { Features = getAllFeatures.Execute().Select(f => f.ToModel()).ToList() }];
            Post["/api/features"] = parameters =>
            {
                saveFeature.Execute(this.Bind<FeatureModel>().ToEntity());
                return HttpStatusCode.OK;
            };
        }
    }
}