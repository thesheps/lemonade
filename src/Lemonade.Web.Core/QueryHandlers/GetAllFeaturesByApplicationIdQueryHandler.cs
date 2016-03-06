using System.Collections.Generic;
using System.Linq;
using Lemonade.Data.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Core.Mappers;
using Lemonade.Web.Core.Queries;

namespace Lemonade.Web.Core.QueryHandlers
{
    public class GetAllFeaturesByApplicationIdQueryHandler : IQueryHandler<GetAllFeaturesByApplicationIdQuery, IList<Feature>>
    {
        public GetAllFeaturesByApplicationIdQueryHandler(IGetAllFeaturesByApplicationId getAllFeaturesByApplicationId)
        {
            _getAllFeaturesByApplicationId = getAllFeaturesByApplicationId;
        }

        public IList<Feature> Handle(GetAllFeaturesByApplicationIdQuery query)
        {
            var features = _getAllFeaturesByApplicationId.Execute(query.ApplicationId);
            return features.Select(f => f.ToContract()).ToList();
        }

        private readonly IGetAllFeaturesByApplicationId _getAllFeaturesByApplicationId;
    }
}