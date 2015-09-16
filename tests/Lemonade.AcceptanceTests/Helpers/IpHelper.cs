using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Lemonade.AcceptanceTests.Helpers
{
    public class IpHelper
    {
        public static Uri GetLocalIpAddress(string port)
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            var address = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);

            return new Uri($"http://{address}:{port}");
        }
    }
}