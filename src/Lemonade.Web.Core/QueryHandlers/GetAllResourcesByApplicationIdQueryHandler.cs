using System.Collections.Generic;
using System.Linq;
using Lemonade.Data.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Core.Mappers;
using Lemonade.Web.Core.Queries;

namespace Lemonade.Web.Core.QueryHandlers
{
    public class GetAllResourcesByApplicationIdQueryHandler : IQueryHandler<GetAllResourcesByApplicationIdQuery, IList<Resource>>
    {
        public GetAllResourcesByApplicationIdQueryHandler(IGetAllResourcesByApplicationId getAllResourcesByApplicationId)
        {
            _getAllResourcesByApplicationId = getAllResourcesByApplicationId;
        }

        public IList<Resource> Handle(GetAllResourcesByApplicationIdQuery query)
        {
            var resources = _getAllResourcesByApplicationId.Execute(query.ApplicationId).Select(r => r.ToContract()).ToList();
            return resources;
        }

        private readonly IGetAllResourcesByApplicationId _getAllResourcesByApplicationId;
    }
}