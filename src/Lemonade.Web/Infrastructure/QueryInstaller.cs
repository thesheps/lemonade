using System.Collections.Generic;
using Lemonade.Web.Contracts;
using Lemonade.Web.Core.Queries;
using Lemonade.Web.Core.QueryHandlers;
using Nancy.TinyIoc;

namespace Lemonade.Web.Infrastructure
{
    public static class QueryInstaller
    {
        public static TinyIoCContainer InstallQueryHandlers(this TinyIoCContainer container)
        {
            container.Register<IQueryHandler<GetAllApplicationsQuery, IList<Application>>, GetAllApplicationsQueryHandler>();
            container.Register<IQueryHandler<GetAllConfigurationsByApplicationIdQuery, IList<Configuration>>, GetAllConfigurationsByApplicationIdQueryHandler>();
            container.Register<IQueryHandler<GetAllFeaturesByApplicationIdQuery, IList<Feature>>, GetAllFeaturesByApplicationIdQueryHandler>();
            container.Register<IQueryHandler<GetAllResourcesByApplicationIdQuery, IList<Resource>>, GetAllResourcesByApplicationIdQueryHandler>();
            container.Register<IQueryHandler<GetAllLocalesQuery, IList<Locale>>, GetAllLocalesQueryHandler>();
            container.Register<IQueryHandler<GetConfigurationByNameAndApplicationQuery, Configuration>, GetConfigurationByNameAndApplicationQueryHandler>();
            container.Register<IQueryHandler<GetFeatureByNameAndApplicationQuery, Feature>, GetFeatureByNameAndApplicationQueryHandler>();
            container.Register<IQueryHandler<GetResourceQuery, Resource>, GetResourceQueryHandler>();

            return container;
        }
    }
}