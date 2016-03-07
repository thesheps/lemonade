using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Lemonade.Web.Contracts;
using Lemonade.Web.Core.Mappers;
using Nancy;

namespace Lemonade.Web.Modules
{
    public class LocalesModules : NancyModule
    {
        public LocalesModules()
        {
            Get["/api/locales"] = r => GetLocales();
        }

        private static IList<Locale> GetLocales()
        {
            var locales = new List<Locale>(new[] { new Locale { Description = "Show all...", IsoCode = "" } });
            locales.AddRange(CultureInfo.GetCultures(CultureTypes.AllCultures).Select(c => c.ToLocale()));

            return locales;
        }
    }
}