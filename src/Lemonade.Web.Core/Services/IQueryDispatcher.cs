using Lemonade.Web.Core.Queries;

namespace Lemonade.Web.Core.Services
{
    public interface IQueryDispatcher
    {
        TResult Dispatch<TResult>(IQuery<TResult> query);
    }
}