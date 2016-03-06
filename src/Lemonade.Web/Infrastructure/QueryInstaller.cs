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

            return container;
        }
    }
}