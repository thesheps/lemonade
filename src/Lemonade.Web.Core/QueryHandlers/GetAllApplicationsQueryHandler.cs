using System.Collections.Generic;
using System.Linq;
using Lemonade.Data.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Core.Mappers;
using Lemonade.Web.Core.Queries;

namespace Lemonade.Web.Core.QueryHandlers
{
    public class GetAllApplicationsQueryHandler : IQueryHandler<GetAllApplicationsQuery, IList<Application>>
    {
        public GetAllApplicationsQueryHandler(IGetAllApplications getAllApplications)
        {
            _getAllApplications = getAllApplications;
        }

        public IList<Application> Handle(GetAllApplicationsQuery query)
        {
            return _getAllApplications.Execute().Select(a => a.ToContract()).ToList();
        }

        private readonly IGetAllApplications _getAllApplications;
    }
}