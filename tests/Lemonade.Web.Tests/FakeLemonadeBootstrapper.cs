using Lemonade.Data.Commands;
using Lemonade.Data.Queries;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Queries;
using Nancy.TinyIoc;

namespace Lemonade.Web.Tests
{
    public class FakeLemonadeBootstrapper : LemonadeBootstrapper
    {
        protected override void ConfigureDependencies(TinyIoCContainer container)
        {
            container.Register<IGetAllFeatures, GetAllFeatures>();
            container.Register<IGetFeatureByNameAndApplication, GetFeatureByNameAndApplication>();
            container.Register<ISaveFeature, SaveFeature>();
            container.Register<IDeleteApplication, DeleteApplication>();
        }
    }
}