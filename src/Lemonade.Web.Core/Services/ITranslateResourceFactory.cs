namespace Lemonade.Web.Core.Services
{
    public interface ITranslateResourceFactory
    {
        ITranslateResource Create(string type);
    }
}