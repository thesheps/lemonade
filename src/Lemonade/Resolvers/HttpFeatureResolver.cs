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

        public bool Get(string featureName)
        {
            var restRequest = new RestRequest("/api/feature") { OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; } };
            restRequest.AddQueryParameter("application", AppDomain.CurrentDomain.FriendlyName);
            restRequest.AddQueryParameter("feature", featureName);

            var response = _restClient.Get<Web.Contracts.Feature>(restRequest);

            if (response.ErrorMessage == "Unable to connect to the remote server")
                throw new ConnectionException(string.Format(Errors.UnableToConnect, _restClient.BaseUrl));

            if (response.StatusCode == HttpStatusCode.InternalServerError)
                throw new ConnectionException(Errors.ServerError, response.ErrorException);

            return response.Data != null && response.Data.IsEnabled;
        }

        private readonly RestClient _restClient;
    }
}