using System.Collections.Generic;
using Lemonade.Web.Contracts;
using Lemonade.Web.Core.Queries;
using Lemonade.Web.Core.Services;
using Nancy;

namespace Lemonade.Web.Modules
{
    public class LocalesModules : NancyModule
    {
        public LocalesModules(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            Get["/api/locales"] = r => GetLocales();
        }

        private IList<Locale> GetLocales()
        {
            return _queryDispatcher.Dispatch(new GetAllLocalesQuery());
        }

        private readonly IQueryDispatcher _queryDispatcher;
    }
}