using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Resources;
using System.Web;
using System.Web.Compilation;
using Lemonade.Core.Exceptions;
using Lemonade.Core.Services;
using Lemonade.Web.Contracts;
using RestSharp;

namespace Lemonade.Services
{
    public class HttpResourceResolver : IResourceResolver
    {
        public IResourceProvider Create(ICacheProvider cacheProvider, string applicationName, string resourceSet)
        {
            return new HttpResourceProvider(cacheProvider, applicationName, resourceSet);
        }

        private class HttpResourceProvider : IResourceProvider
        {
            public IResourceReader ResourceReader { get; }

            public HttpResourceProvider(ICacheProvider cacheProvider, string applicationName, string resourceSet) : this(cacheProvider, applicationName, resourceSet, ConfigurationManager.AppSettings["LemonadeServiceUrl"])
            {
            }

            private HttpResourceProvider(ICacheProvider cacheProvider, string applicationName, string resourceSet, string lemonadeServiceUri) : this(cacheProvider, applicationName, resourceSet, new Uri(lemonadeServiceUri))
            {
            }

            private HttpResourceProvider(ICacheProvider cacheProvider, string applicationName, string resourceSet, Uri lemonadeServiceUri)
            {
                _cacheProvider = cacheProvider;
                _applicationName = applicationName;
                _resourceSet = resourceSet;
                _restClient = new RestClient(lemonadeServiceUri);
            }

            public object GetObject(string resourceKey, CultureInfo culture)
            {
                var locale = (culture ?? GetCurrentUserCulture()).ToString();
                var key = $"Resource{_applicationName}|{_resourceSet}|{resourceKey}|{locale}";

                return _cacheProvider.GetValue(key, () =>
                {
                    var restRequest = new RestRequest("/api/resource") { OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; } };
                    restRequest.AddQueryParameter("application", _applicationName);
                    restRequest.AddQueryParameter("resourceSet", _resourceSet);
                    restRequest.AddQueryParameter("resourceKey", resourceKey);
                    restRequest.AddQueryParameter("locale", locale);

                    var response = _restClient.Get<Resource>(restRequest);

                    if (response.ErrorMessage == "Unable to connect to the remote server")
                        throw new HttpConnectionException(string.Format(Errors.UnableToConnect, _restClient.BaseUrl), response.ErrorException);

                    if (response.StatusCode == HttpStatusCode.InternalServerError)
                        throw new HttpConnectionException(Errors.ServerError, response.ErrorException);

                    return response.Data.Value;
                });
            }

            private static CultureInfo GetCurrentUserCulture()
            {
                var userLanguages = HttpContext.Current.Request.UserLanguages;

                if (userLanguages != null && !userLanguages.Any()) return CultureInfo.InvariantCulture;

                try
                {
                    return new CultureInfo(userLanguages[0]);
                }
                catch (CultureNotFoundException)
                {
                    return CultureInfo.InvariantCulture;
                }
            }

            private readonly ICacheProvider _cacheProvider;
            private readonly string _applicationName;
            private readonly string _resourceSet;
            private readonly RestClient _restClient;
        }
    }
}