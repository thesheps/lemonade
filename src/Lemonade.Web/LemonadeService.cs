using System;
using Lemonade.Data.Commands;
using Lemonade.Data.Queries;
using Nancy.Bootstrapper;
using Nancy.Hosting.Self;

namespace Lemonade.Web
{
    public class LemonadeService : IDisposable
    {
        public LemonadeService(string hostingUri, IGetAllFeatures getAllFeatures, IGetFeatureByNameAndApplication getFeatureByNameAndApplication, ISaveFeature saveFeature)
            : this(new Uri(hostingUri), getAllFeatures, getFeatureByNameAndApplication, saveFeature)
        {
        }

        public LemonadeService(Uri hostingUri, IGetAllFeatures getAllFeatures, IGetFeatureByNameAndApplication getFeatureByNameAndApplication, ISaveFeature saveFeature)
        {
            _hostingUri = hostingUri;
            _bootstrapper = new Bootstrapper(getAllFeatures, getFeatureByNameAndApplication, saveFeature);
        }

        public void Start()
        {
            var urlReservations = new UrlReservations { CreateAutomatically = true };
            _host = new NancyHost(_bootstrapper, new HostConfiguration { UrlReservations = urlReservations }, _hostingUri);
            _host.Start();
        }

        public void Dispose()
        {
            _bootstrapper.Dispose();
            _host.Stop();
            _host.Dispose();
        }

        private readonly Uri _hostingUri;
        private readonly INancyBootstrapper _bootstrapper;
        private NancyHost _host;
    }
}