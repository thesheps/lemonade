using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Lemonade.Core.Exceptions;
using Lemonade.Core.Services;
using Lemonade.Exceptions;
using Newtonsoft.Json;

namespace Lemonade.Services
{
    public class HttpFeatureResolver : IFeatureResolver
    {
        public HttpFeatureResolver(string lemonadeServiceUri) : this(new Uri(lemonadeServiceUri))
        {
        }

        public HttpFeatureResolver(Uri lemonadeServiceUri)
        {
            _restClient = new HttpClient { BaseAddress = lemonadeServiceUri };
        }

        public bool Resolve(string featureName, string applicationName)
        {
            var feature = GetFeature(featureName, applicationName);
            var featureOverride = feature.Result.FeatureOverrides?.FirstOrDefault(f => f.Hostname == Dns.GetHostName());

            return featureOverride?.IsEnabled ?? feature.Result.IsEnabled;
        }

        private async Task<Web.Contracts.Feature> GetFeature(string featureName, string applicationName)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/feature");
            requestMessage.Headers.Add("feature", featureName);
            requestMessage.Headers.Add("application", applicationName);

            try
            {
                var response = await _restClient.SendAsync(requestMessage);
                var jsonString = await response.Content.ReadAsStringAsync();
                var feature = JsonConvert.DeserializeObject<Web.Contracts.Feature>(jsonString);

                if (feature == null)
                    throw new FeatureCouldNotBeFoundException();

                return feature;
            }
            catch (Exception ex)
            {
                throw new HttpConnectionException(Errors.ServerError, ex);
            }
        }

        private readonly HttpClient _restClient;
    }
}