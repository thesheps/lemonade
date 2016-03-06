using System.Collections.Generic;
using Lemonade.Web.Contracts;

namespace Lemonade.Web.Core.Queries
{
    public class GetAllFeaturesByApplicationIdQuery : IQuery<IList<Feature>>
    {
        public int ApplicationId { get; }

        public GetAllFeaturesByApplicationIdQuery(int applicationId)
        {
            ApplicationId = applicationId;
        }
    }
}