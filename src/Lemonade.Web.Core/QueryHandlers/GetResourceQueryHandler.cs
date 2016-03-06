using Lemonade.Data.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Core.Mappers;
using Lemonade.Web.Core.Queries;

namespace Lemonade.Web.Core.QueryHandlers
{
    public class GetResourceQueryHandler : IQueryHandler<GetResourceQuery, Resource>
    {
        public GetResourceQueryHandler(IGetResource getResource)
        {
            _getResource = getResource;
        }

        public Resource Handle(GetResourceQuery query)
        {
            var resource = _getResource.Execute(query.Application, query.ResourceSet, query.ResourceKey, query.Locale);
            return resource.ToContract();
        }

        private readonly IGetResource _getResource;
    }
}