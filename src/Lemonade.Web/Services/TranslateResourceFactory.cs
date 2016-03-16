using Lemonade.Web.Core.Exceptions;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Services
{
    public class TranslateResourceFactory : ITranslateResourceFactory
    {
        public ITranslateResource Create(string type)
        {
            switch (type)
            {
                case "pseudo":
                    return new PseudoResourceTranslator();
                case "google":
                    return new GoogleResourceTranslator();
                default:
                    throw new UnsupportedTranslationException(type);
            }
        }
    }
}