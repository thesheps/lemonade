using System.Configuration;
using Lemonade.Data.Queries;
using Lemonade.SqlServer;
using Lemonade.SqlServer.Queries;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace Lemonade.Web.Host
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            DbMigrations.Run(ConfigurationManager.ConnectionStrings["Lemonade"].ConnectionString);
            container.Register<IGetAllFeatures>((pp, c) => new GetAllFeatures(ConfigurationManager.ConnectionStrings["Lemonade"].ConnectionString));
        }

        protected override void ConfigureConventions(Nancy.Conventions.NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);
            nancyConventions.ViewLocationConventions.Add((viewName, model, context) => string.Concat("bin/views/", viewName));
            nancyConventions.StaticContentsConventions.Add(Nancy.Conventions.StaticContentConventionBuilder.AddDirectory("assets", "bin/content/"));
        }
    }
}