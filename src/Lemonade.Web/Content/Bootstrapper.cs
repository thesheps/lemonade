using Nancy.TinyIoc;
using Lemonade.Sql.Queries;
using Lemonade.Sql.Commands;
using Lemonade.Core.Queries;
using Lemonade.Core.Commands;
using Lemonade.Web.Infrastructure;

namespace Lemonade.Web.Content
{
    /// <summary>
    /// Implement the ConfigureDependencies method of this class to specify which data implementation
    /// you want to use, plus any connection strings you may want to specify.  For more information
    /// on TinyIoC configuration, please see <see href="https://github.com/grumpydev/TinyIoC/wiki/Setup---getting-started">HERE</see>
    /// </summary>
    public class Bootstrapper : LemonadeBootstrapper
    {
        protected override void ConfigureDependencies(TinyIoCContainer container)
        {
            //ToDo: Configure implementations for the following three interfaces.
            //container.Register<IGetAllFeatures, GetAllFeatures>();
            //container.Register<IGetFeatureByNameAndApplication, GetFeatureByNameAndApplication>();
            //container.Register<ISaveFeature, SaveFeature>();
        }
    }
}