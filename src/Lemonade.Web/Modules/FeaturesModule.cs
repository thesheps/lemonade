using System.Linq;
using Lemonade.Data.Queries;
using Lemonade.Web.Mappers;
using Lemonade.Web.Models;
using Nancy;
using Nancy.ModelBinding;

namespace Lemonade.Web.Modules
{
    public class FeaturesModule : NancyModule
    {
        public FeaturesModule(IGetAllFeatures getAllFeatures)
        {
            _getAllFeatures = getAllFeatures;
            Get["/features"] = parameters => View[new FeaturesModel { Features = _getAllFeatures.Execute().Select(f => f.ToModel()).ToList() }];
            Post["/features"] = parameters => HttpStatusCode.OK;
        }

        private readonly IGetAllFeatures _getAllFeatures;
    }
}