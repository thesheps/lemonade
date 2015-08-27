using System.Linq;
using Lemonade.Data.Queries;
using Lemonade.Web.Mappers;
using Lemonade.Web.Models;
using Nancy;

namespace Lemonade.Web.Modules
{
    public class FeaturesModule : NancyModule
    {
        public FeaturesModule(IGetAllFeatures getAllFeatures)
        {
            _getAllFeatures = getAllFeatures;
            Get["/features"] = parameters => View[new FeaturesModel { Features = _getAllFeatures.Execute().Select(f => f.ToModel()).ToList() }];
        }

        private readonly IGetAllFeatures _getAllFeatures;
    }
}