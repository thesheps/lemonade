using Lemonade.Core.Commands;
using Lemonade.Core.Queries;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Migrations;
using Lemonade.Sql.Queries;
using Lemonade.Web.Infrastructure;
using Nancy.TinyIoc;

namespace Lemonade.Web.Host
{
    public class Bootstrapper : LemonadeBootstrapper
    {
        public Bootstrapper()
        {
            Runner.SqlServer("Lemonade").Up();
        }

        protected override void ConfigureDependencies(TinyIoCContainer container)
        {
            container.Register<IGetAllFeatures, GetAllFeatures>();
            container.Register<IGetFeatureByNameAndApplication, GetFeatureByNameAndApplication>();
            container.Register<ISaveFeature, SaveFeature>();
        }
    }
}