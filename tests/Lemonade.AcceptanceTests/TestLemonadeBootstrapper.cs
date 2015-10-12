using Lemonade.Data.Commands;
using Lemonade.Data.Queries;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Queries;
using Lemonade.Web.Infrastructure;
using Microsoft.AspNet.SignalR;
using Nancy.TinyIoc;

namespace Lemonade.AcceptanceTests
{
    public class TestLemonadeBootstrapper : LemonadeBootstrapper
    {
        protected override void ConfigureDependencies(TinyIoCContainer container)
        {
            container.Register(GlobalHost.ConnectionManager);
            container.Register<IGetAllFeatures, GetAllFeatures>();
            container.Register<IGetFeatureByNameAndApplication, GetFeatureByNameAndApplication>();
            container.Register<ICreateFeature, CreateFeature>();
        }
    }
}