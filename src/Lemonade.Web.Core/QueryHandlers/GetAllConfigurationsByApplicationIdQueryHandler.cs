using System.Collections.Generic;
using System.Linq;
using Lemonade.Data.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Core.Mappers;
using Lemonade.Web.Core.Queries;

namespace Lemonade.Web.Core.QueryHandlers
{
    public class GetAllConfigurationsByApplicationIdQueryHandler : IQueryHandler<GetAllConfigurationsByApplicationIdQuery, IList<Configuration>>
    {
        public GetAllConfigurationsByApplicationIdQueryHandler(IGetAllConfigurationsByApplicationId getAllConfigurationsByApplicationId)
        {
            _getAllConfigurationsByApplicationId = getAllConfigurationsByApplicationId;
        }

        public IList<Configuration> Handle(GetAllConfigurationsByApplicationIdQuery query)
        {
            return _getAllConfigurationsByApplicationId.Execute(query.ApplicationId).Select(c => c.ToContract()).ToList();
        }

        private readonly IGetAllConfigurationsByApplicationId _getAllConfigurationsByApplicationId;
    }
}