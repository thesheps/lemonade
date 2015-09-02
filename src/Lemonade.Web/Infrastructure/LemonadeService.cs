using System;
using Nancy.Hosting.Self;

namespace Lemonade.Web.Infrastructure
{
    public class LemonadeService : IDisposable
    {
        public LemonadeService(string hostingUrl)
        {
            _hostingUrl = hostingUrl;
        }

        public void Start()
        {
            var urlReservations = new UrlReservations { CreateAutomatically = true };
            _host = new NancyHost(new HostConfiguration { UrlReservations = urlReservations }, new Uri(_hostingUrl));
            _host.Start();
        }

        public void Dispose()
        {
            _host.Stop();
            _host.Dispose();
        }

        private readonly string _hostingUrl;
        private NancyHost _host;
    }
}