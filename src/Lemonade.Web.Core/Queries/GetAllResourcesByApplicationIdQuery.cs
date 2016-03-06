using System.Collections.Generic;
using Lemonade.Web.Contracts;

namespace Lemonade.Web.Core.Queries
{
    public class GetAllResourcesByApplicationIdQuery : IQuery<IList<Resource>>
    {
        public int ApplicationId { get; }

        public GetAllResourcesByApplicationIdQuery(int applicationId)
        {
            ApplicationId = applicationId;
        }
    }
}