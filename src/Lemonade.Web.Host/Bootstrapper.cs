using Lemonade.Sql.Migrations;
using Lemonade.Web.Infrastructure;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace Lemonade.Web.Host
{
    public class Bootstrapper : LemonadeBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            Runner.SqlServer("Lemonade").Up();
        }
    }
}