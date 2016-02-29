using System;
using System.Configuration;
using System.Globalization;
using System.Net;
using System.Resources;
using System.Web.Compilation;
using Lemonade.Core.Exceptions;
using Lemonade.Core.Services;
using Lemonade.Web.Contracts;
using RestSharp;

namespace Lemonade.Services
{
    public class HttpResourceResolver : IResourceResolver
    {
        public IResourceProvider Create(string resourceSet)
        {
            return new HttpResourceProvider(resourceSet);
        }

        private class HttpResourceProvider : IResourceProvider
        {
            public IResourceReader ResourceReader { get; }

            public HttpResourceProvider(string resourceSet) : this(resourceSet, ConfigurationManager.AppSettings["LemonadeServiceUrl"])
            {
            }

            private HttpResourceProvider(string resourceSet, string lemonadeServiceUri) : this(resourceSet, new Uri(lemonadeServiceUri))
            {
            }

            private HttpResourceProvider(string resourceSet, Uri lemonadeServiceUri)
            {
                _resourceSet = resourceSet;
                _restClient = new RestClient(lemonadeServiceUri);
            }

            public object GetObject(string resourceKey, CultureInfo culture)
            {
                var locale = (culture ?? CultureInfo.CurrentCulture).ToString();
                var restRequest = new RestRequest("/api/resource") { OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; } };
                restRequest.AddQueryParameter("application", Configuration.ApplicationName);
                restRequest.AddQueryParameter("resourceSet", _resourceSet);
                restRequest.AddQueryParameter("resourceKey", resourceKey);
                restRequest.AddQueryParameter("locale", locale);

                var response = _restClient.Get<Resource>(restRequest);

                if (response.ErrorMessage == "Unable to connect to the remote server")
                    throw new HttpConnectionException(string.Format(Errors.UnableToConnect, _restClient.BaseUrl), response.ErrorException);

                if (response.StatusCode == HttpStatusCode.InternalServerError)
                    throw new HttpConnectionException(Errors.ServerError, response.ErrorException);

                return response.Data.Value;
            }

            private readonly string _resourceSet;
            private readonly RestClient _restClient;
        }
    }
}