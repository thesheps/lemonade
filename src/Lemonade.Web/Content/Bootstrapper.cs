using Nancy.TinyIoc;
using Lemonade.Data.Queries;
using Lemonade.Data.Commands;
using Lemonade.Web.Infrastructure;

namespace Lemonade.Web.Content
{
    /// <summary>
    /// By default, Lemonade's IoC configuration will automagically discover any command/query implementations at runtime.
    /// However, if there are any additional dependencies you need to configure this can be done by overriding the method below.
    /// For more information on TinyIoC configuration, please see <see href="https://github.com/grumpydev/TinyIoC/wiki/Setup---getting-started">HERE</see>
    /// </summary>
    public class Bootstrapper : LemonadeBootstrapper
    {
        protected override void ConfigureDependencies(TinyIoCContainer container)
        {
            //container.Register<IGetAllFeatures, GetAllFeatures>();
            //container.Register<IGetFeatureByNameAndApplication, GetFeatureByNameAndApplication>();
            //container.Register<ICreateFeature, CreateFeature>();
        }
    }
}