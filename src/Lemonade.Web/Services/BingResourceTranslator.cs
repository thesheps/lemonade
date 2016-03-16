using System.Net;
using Lemonade.Web.Core.Services;
using RestSharp;

namespace Lemonade.Web.Services
{
    public class BingResourceTranslator : ITranslateResource
    {
        public BingResourceTranslator(string baseUrl, string sharedSecret)
        {
            _baseUrl = baseUrl;
            _sharedSecret = sharedSecret;
        }

        public string Translate(string resource, string locale, string targetLocale)
        {
            var restClient = new RestClient(_baseUrl);
            var restRequest = new RestRequest("Translate") { OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; } };
            restRequest.AddQueryParameter("text", resource);
            restRequest.AddQueryParameter("from", locale);
            restRequest.AddQueryParameter("to", targetLocale);
            restRequest.AddQueryParameter("Authorization", $"Bearer {_sharedSecret}");

            return restClient.Get(restRequest).Content;
        }

        private readonly string _baseUrl;
        private readonly string _sharedSecret;
    }
}