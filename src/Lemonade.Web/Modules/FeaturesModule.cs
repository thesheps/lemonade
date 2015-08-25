using System.Linq;
using Lemonade.Data.Queries;
using Lemonade.Web.Mappers;
using Nancy;

namespace Lemonade.Web.Modules
{
    public class FeaturesModule : NancyModule
    {
        public FeaturesModule(IGetAllFeatures getAllFeatures)
        {
            _getAllFeatures = getAllFeatures;
            Get["/features"] = parameters => View[_getAllFeatures.Execute().Select(f => f.ToModel())];
        }

        private readonly IGetAllFeatures _getAllFeatures;
    }
}