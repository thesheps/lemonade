using System;
using System.Configuration;
using System.Net;
using Lemonade.Exceptions;
using RestSharp;

namespace Lemonade.Resolvers
{
    public class HttpFeatureResolver : IFeatureResolver
    {
        public HttpFeatureResolver() : this(ConfigurationManager.AppSettings["LemonadeServiceUrl"])
        {
        }

        public HttpFeatureResolver(string lemonadeServiceUri) : this(new Uri(lemonadeServiceUri))
        {
        }

        public HttpFeatureResolver(Uri lemonadeServiceUri)
        {
            _restClient = new RestClient(lemonadeServiceUri);
        }

        public bool Resolve(string featureName, string applicationName)
        {
            var response = GetFeature(featureName, applicationName);
            return response.IsEnabled;
        }

        private Web.Contracts.Feature GetFeature(string featureName, string applicationName)
        {
            var restRequest = new RestRequest("/api/feature") { OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; } };
            restRequest.AddQueryParameter("feature", featureName);
            restRequest.AddQueryParameter("application", applicationName);

            var response = _restClient.Get<Web.Contracts.Feature>(restRequest);

            if (response.ErrorMessage == "Unable to connect to the remote server")
                throw new HttpConnectionException(string.Format(Errors.UnableToConnect, _restClient.BaseUrl), response.ErrorException);

            if (response.StatusCode == HttpStatusCode.InternalServerError)
                throw new HttpConnectionException(Errors.ServerError, response.ErrorException);

            return response.Data;
        }

        private readonly RestClient _restClient;
    }
}