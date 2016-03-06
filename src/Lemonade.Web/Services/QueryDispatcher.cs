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

        public TResult Dispatch<TResult>(IQuery<TResult> query)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            var handler = _container.Resolve(handlerType);

            return (TResult)((dynamic)handler).Handle((dynamic)query);
        }

        private readonly TinyIoCContainer _container;
    }
}