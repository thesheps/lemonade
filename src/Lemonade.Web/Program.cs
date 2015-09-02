using System.Configuration;
using Topshelf;

namespace Lemonade.Web
{
    public class Program
    {
        private static void Main()
        {
            HostFactory.Run(configurator =>
            {
                configurator.Service<LemonadeService>(s => s.ConstructUsing(n => new LemonadeService("")));
                configurator.RunAsNetworkService();
                configurator.SetDescription("Lemonade");
                configurator.SetDisplayName(ConfigurationManager.AppSettings["LemonadeServiceName"]);
                configurator.SetServiceName(ConfigurationManager.AppSettings["LemonadeServiceName"]);
                configurator.EnableServiceRecovery(rc =>
                {
                    rc.RestartService(0);
                    rc.SetResetPeriod(1);
                });
            });
        }
    }
}