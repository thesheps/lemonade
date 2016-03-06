using Lemonade.Data.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Core.Mappers;
using Lemonade.Web.Core.Queries;

namespace Lemonade.Web.Core.QueryHandlers
{
    public class GetConfigurationByNameAndApplicationQueryHandler : IQueryHandler<GetConfigurationByNameAndApplicationQuery, Configuration>
    {
        public GetConfigurationByNameAndApplicationQueryHandler(IGetConfigurationByNameAndApplication getConfigurationByNameAndApplication)
        {
            _getConfigurationByNameAndApplication = getConfigurationByNameAndApplication;
        }

        public Configuration Handle(GetConfigurationByNameAndApplicationQuery query)
        {
            var configuration = _getConfigurationByNameAndApplication.Execute(query.ConfigurationName, query.ApplicationName);
            return configuration.ToContract();
        }

        private readonly IGetConfigurationByNameAndApplication _getConfigurationByNameAndApplication;
    }
}