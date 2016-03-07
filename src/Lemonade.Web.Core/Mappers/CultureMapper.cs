using System.Globalization;
using Lemonade.Web.Contracts;

namespace Lemonade.Web.Core.Mappers
{
    public static class CultureMapper
    {
        public static Locale ToLocale(this CultureInfo cultureInfo)
        {
            return new Locale()
            {
                Description = cultureInfo.EnglishName,
                IsoCode = cultureInfo.Name
            };
        }
    }
}