using Lemonade.Core.Events;
using Lemonade.Web;
using Lemonade.Web.Infrastructure;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Newtonsoft.Json;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace Lemonade.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            GlobalHost.DependencyResolver.Register(typeof(JsonSerializer), () => new JsonSerializer
            {
                ContractResolver = new CamelCasePropertyNameContractResolver { AssembliesToInclude = { typeof(ApplicationHasBeenCreated).Assembly } }
            });
        }
    }
}