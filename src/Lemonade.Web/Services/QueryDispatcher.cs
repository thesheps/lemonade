using Lemonade.Web.Core.Queries;
using Lemonade.Web.Core.QueryHandlers;
using Lemonade.Web.Core.Services;
using Nancy.TinyIoc;

namespace Lemonade.Web.Services
{
    public class QueryDispatcher : IQueryDispatcher
    {
        public QueryDispatcher(TinyIoCContainer container)
        {
            _container = container;
        }

        public TResult Dispatch<TQuery, TResult>(TQuery query) where TQuery : IQuery
        {
            return _container.Resolve<IQueryHandler<TQuery, TResult>>().Handle(query);
        }

        private readonly TinyIoCContainer _container;
    }
}