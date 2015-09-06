using System.Linq;
using Lemonade.Data.Commands;
using Lemonade.Data.Queries;
using Lemonade.Web.Mappers;
using Lemonade.Web.Models;
using Nancy;
using Nancy.ModelBinding;

namespace Lemonade.Web.Modules
{
    public class FeaturesModule : NancyModule
    {
        public FeaturesModule(IGetAllFeatures getAllFeatures, ISaveFeature saveFeature)
        {
            Get["/features"] = parameters => View[new FeaturesModel { Features = getAllFeatures.Execute().Select(f => f.ToModel()).ToList() }];
            Post["/features"] = parameters =>
            {
                saveFeature.Execute(this.Bind<FeatureModel>().ToEntity());
                return HttpStatusCode.OK;
            };
        }
    }
}