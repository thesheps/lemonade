using System.Configuration;
using Lemonade.Web.Contracts;
using RestSharp;

namespace Lemonade.Resolvers
{
    public class HttpFeatureResolver : IFeatureResolver
    {
        public HttpFeatureResolver() : this(ConfigurationManager.AppSettings["LemonadeServiceUrl"])
        {
        }

        public HttpFeatureResolver(string lemonadeServiceUrl)
        {
            _restClient = new RestClient(lemonadeServiceUrl);
        }

        public bool Get(string value)
        {
            var restRequest = new RestRequest("/api/feature")
            {
                OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; }
            };

            var response = _restClient.Get<FeatureModel>(restRequest);
            return response.Data.IsEnabled;
        }

        private readonly RestClient _restClient;
    }
}