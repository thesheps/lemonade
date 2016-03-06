using System.Collections.Generic;
using Lemonade.Web.Contracts;

namespace Lemonade.Web.Core.Queries
{
    public class GetAllConfigurationsByApplicationIdQuery : IQuery<IList<Configuration>>
    {
        public int ApplicationId { get; }

        public GetAllConfigurationsByApplicationIdQuery(int applicationId)
        {
            ApplicationId = applicationId;
        }
    }
}