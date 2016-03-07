using System.Collections.Generic;
using System.Linq;
using Lemonade.Data.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Core.Mappers;
using Lemonade.Web.Core.Queries;

namespace Lemonade.Web.Core.QueryHandlers
{
    public class GetAllLocalesQueryHandler : IQueryHandler<GetAllLocalesQuery, IList<Locale>>
    {
        public GetAllLocalesQueryHandler(IGetAllLocales getAllLocales)
        {
            _getAllLocales = getAllLocales;
        }

        public IList<Locale> Handle(GetAllLocalesQuery query)
        {
            return _getAllLocales.Execute().Select(l => l.ToContract()).ToList();
        }

        private readonly IGetAllLocales _getAllLocales;
    }
}