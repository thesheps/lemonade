using System;
using System.Configuration;
using System.Net;
using Lemonade.Core.Exceptions;
using Lemonade.Core.Services;
using RestSharp;

namespace Lemonade.Services
{
    public class HttpConfigurationResolver : IConfigurationResolver
    {
        public HttpConfigurationResolver() : this(ConfigurationManager.AppSettings["LemonadeServiceUrl"])
        {
        }

        public HttpConfigurationResolver(string lemonadeServiceUri) : this(new Uri(lemonadeServiceUri))
        {
        }

        public HttpConfigurationResolver(Uri lemonadeServiceUri)
        {
            _restClient = new RestClient(lemonadeServiceUri);
        }

        public T Resolve<T>(string configurationName, string applicationName)
        {
            var restRequest = new RestRequest("/api/configuration") { OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; } };
            restRequest.AddQueryParameter("configuration", configurationName);
            restRequest.AddQueryParameter("application", applicationName);

            var response = _restClient.Get<Web.Contracts.Configuration>(restRequest);

            if (response.ErrorMessage == "Unable to connect to the remote server")
                throw new HttpConnectionException(string.Format(Errors.UnableToConnect, _restClient.BaseUrl), response.ErrorException);

            if (response.StatusCode == HttpStatusCode.InternalServerError)
                throw new HttpConnectionException(Errors.ServerError, response.ErrorException);

            return GetValue<T>(response.Data.Value);
        }

        private T GetValue<T>(string value)
        {
            if (typeof(T) == typeof(Uri))
                return (T)((object)new Uri(value));

            return (T)Convert.ChangeType(value, typeof(T));
        }

        private readonly RestClient _restClient;
    }
}