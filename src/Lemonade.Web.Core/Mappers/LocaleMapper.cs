using Lemonade.Web.Contracts;

namespace Lemonade.Web.Core.Mappers
{
    public static class LocaleMapper
    {
        public static Locale ToContract(this Data.Entities.Locale locale)
        {
            return new Locale
            {
                LocaleId = locale.LocaleId,
                IsoCode = locale.IsoCode,
                Description = locale.Description
            };
        }

        public static Data.Entities.Locale ToEntity(this Locale locale)
        {
            return new Data.Entities.Locale
            {
                LocaleId = locale.LocaleId,
                IsoCode = locale.IsoCode,
                Description = locale.Description
            };
        }
    }
}