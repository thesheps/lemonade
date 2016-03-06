using Lemonade.Data.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Core.Mappers;
using Lemonade.Web.Core.Queries;

namespace Lemonade.Web.Core.QueryHandlers
{
    public class GetFeatureByNameAndApplicationQueryHandler : IQueryHandler<GetFeatureByNameAndApplicationQuery, Feature>
    {
        public GetFeatureByNameAndApplicationQueryHandler(IGetFeatureByNameAndApplication getFeatureByNameAndApplication)
        {
            _getFeatureByNameAndApplication = getFeatureByNameAndApplication;
        }

        public Feature Handle(GetFeatureByNameAndApplicationQuery query)
        {
            var feature = _getFeatureByNameAndApplication.Execute(query.FeatureName, query.ApplicationName);

            return feature?.ToContract();
        }

        private readonly IGetFeatureByNameAndApplication _getFeatureByNameAndApplication;
    }
}