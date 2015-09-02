using System.Configuration;
using Lemonade.Web.Infrastructure;
using Topshelf;

namespace Lemonade.Web
{
    public class Program
    {
        private static void Main()
        {
            HostFactory.Run(configurator =>
            {
                configurator.Service<LemonadeService>(s =>
                {
                    s.ConstructUsing(n => new LemonadeService(ConfigurationManager.AppSettings["LemonadeUrl"]));
                    s.WhenStarted(svc => svc.Start());
                    s.WhenStopped(svc => svc.Dispose());
                });

                configurator.RunAsNetworkService();
                configurator.SetDescription("Lemonade Web Service");
                configurator.SetDisplayName(ConfigurationManager.AppSettings["LemonadeServiceName"]);
                configurator.SetServiceName(ConfigurationManager.AppSettings["LemonadeServiceName"]);
            });
        }
    }
}