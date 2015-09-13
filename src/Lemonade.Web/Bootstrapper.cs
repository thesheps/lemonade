using Lemonade.Data.Commands;
using Lemonade.Data.Queries;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace Lemonade.Web
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        public Bootstrapper(IGetAllFeatures getAllFeatures, IGetFeatureByNameAndApplication getFeatureByNameAndApplication, ISaveFeature saveFeature)
        {
            _getAllFeatures = getAllFeatures;
            _getFeatureByNameAndApplication = getFeatureByNameAndApplication;
            _saveFeature = saveFeature;
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            container.Register(_getAllFeatures);
            container.Register(_getFeatureByNameAndApplication);
            container.Register(_saveFeature);
        }

        private readonly IGetAllFeatures _getAllFeatures;
        private readonly IGetFeatureByNameAndApplication _getFeatureByNameAndApplication;
        private readonly ISaveFeature _saveFeature;
    }
}