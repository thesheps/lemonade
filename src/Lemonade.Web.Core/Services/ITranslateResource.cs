namespace Lemonade.Web.Core.Services
{
    public interface ITranslateResource
    {
        string Translate(string resource, string locale, string targetLocale);
    }
}