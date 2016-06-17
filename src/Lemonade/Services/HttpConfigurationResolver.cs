using System;
using System.Net.Http;
using System.Threading.Tasks;
using Lemonade.Core.Services;
using Lemonade.Exceptions;
using Newtonsoft.Json;

namespace Lemonade.Services
{
    public class HttpConfigurationResolver : IConfigurationResolver
    {
        public HttpConfigurationResolver(string lemonadeServiceUri) : this(new Uri(lemonadeServiceUri))
        {
        }

        public HttpConfigurationResolver(Uri lemonadeServiceUri)
        {
            _restClient = new HttpClient { BaseAddress = lemonadeServiceUri };
        }

        public T Resolve<T>(string configurationName, string applicationName)
        {
            return GetConfiguration<T>(configurationName, applicationName).Result;
        }

        private async Task<T> GetConfiguration<T>(string configurationName, string applicationName)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/configuration");
            requestMessage.Headers.Add("configuration", configurationName);
            requestMessage.Headers.Add("application", applicationName);

            try
            {
                var response = await _restClient.SendAsync(requestMessage);
                var jsonString = await response.Content.ReadAsStringAsync();
                var configuration = JsonConvert.DeserializeObject<Web.Contracts.Configuration>(jsonString);

                return GetValue<T>(configuration.Value);
            }
            catch (Exception ex)
            {
                throw new HttpConnectionException(Errors.ServerError, ex);
            }
        }

        private static T GetValue<T>(string value)
        {
            if (typeof(T) == typeof(Uri))
                return (T)((object)new Uri(value));

            return (T)Convert.ChangeType(value, typeof(T));
        }

        private readonly HttpClient _restClient;
    }
}