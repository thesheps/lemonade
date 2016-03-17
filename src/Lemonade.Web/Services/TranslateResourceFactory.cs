using System.Configuration;
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
                case "bing":
                    return new BingResourceTranslator(
                        ConfigurationManager.AppSettings["TranslationClientId"], 
                        ConfigurationManager.AppSettings["TranslationUrl"],
                        ConfigurationManager.AppSettings["TranslationSecret"]);
                default:
                    throw new UnsupportedTranslationException(type);
            }
        }
    }
}