using Lemonade.Web.Core.Services;
using RestSharp;

namespace Lemonade.Web.Services
{
    public class BingResourceTranslator : ITranslateResource
    {
        public BingResourceTranslator(string clientId, string baseUrl, string sharedSecret)
        {
            _baseUrl = baseUrl;
            _accessToken = GetAccessToken(clientId, sharedSecret);
        }

        public string Translate(string resource, string locale, string targetLocale)
        {
            var restClient = new RestClient(_baseUrl);
            var request = new RestRequest("Translate") { OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; } };
            request.AddQueryParameter("text", resource);
            request.AddQueryParameter("from", locale);
            request.AddQueryParameter("to", targetLocale);
            request.AddHeader("Authorization", $"Bearer {_accessToken.access_token}");

            var response = restClient.Get(request);
            return response.Content.Replace("\"", "");
        }

        private static AdmAccessToken GetAccessToken(string clientId, string clientSecret)
        {
            const string accessUri = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";
            var restClient = new RestClient(accessUri);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_id", clientId);
            request.AddParameter("client_secret", clientSecret);
            request.AddParameter("scope", "http://api.microsofttranslator.com");

            var token = restClient.Post<AdmAccessToken>(request);

            return token.Data;
        }

        private readonly string _baseUrl;
        private readonly AdmAccessToken _accessToken;

        private class AdmAccessToken
        {
            public string access_token { get; set; }
        }
    }
}