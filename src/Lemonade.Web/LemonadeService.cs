using System;
using Nancy.Bootstrapper;
using Nancy.Hosting.Self;

namespace Lemonade.Web
{
    public class LemonadeService : IDisposable
    {
        public LemonadeService(INancyBootstrapper bootstrapper, string hostingUrl)
        {
            _bootstrapper = bootstrapper;
            _hostingUrl = hostingUrl;
        }

        public void Start()
        {
            var urlReservations = new UrlReservations { CreateAutomatically = true };
            _host = new NancyHost(_bootstrapper, 
                new HostConfiguration { UrlReservations = urlReservations }, 
                new Uri(_hostingUrl));

            _host.Start();
        }

        public void Dispose()
        {
            _bootstrapper.Dispose();
            _host.Stop();
            _host.Dispose();
        }

        private readonly string _hostingUrl;
        private readonly INancyBootstrapper _bootstrapper;
        private NancyHost _host;
    }
}