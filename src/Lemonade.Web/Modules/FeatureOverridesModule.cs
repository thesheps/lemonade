using Lemonade.Data.Commands;
using Lemonade.Data.Exceptions;
using Lemonade.Data.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Core.Events;
using Lemonade.Web.Mappers;
using Nancy;
using Nancy.ModelBinding;

namespace Lemonade.Web.Modules
{
    public class FeatureOverridesModule : NancyModule
    {
        public FeatureOverridesModule(IDomainEventDispatcher eventDispatcher, ICreateFeatureOverride createFeatureOverride, IUpdateFeatureOverride updateFeatureOverride, IDeleteFeatureOverride deleteFeatureOverride)
        {
            _eventDispatcher = eventDispatcher;
            _createFeatureOverride = createFeatureOverride;
            _updateFeatureOverride = updateFeatureOverride;
            _deleteFeatureOverride = deleteFeatureOverride;
            Post["/api/featureoverrides"] = p => CreateFeatureOverride();
            Put["/api/featureoverrides"] = p => UpdateFeatureOverride();
            Delete["/api/featureoverrides"] = p => DeleteFeatureOverride();
        }

        private HttpStatusCode CreateFeatureOverride()
        {
            try
            {
                var featureOverride = this.Bind<FeatureOverride>().ToEntity();
                _createFeatureOverride.Execute(featureOverride);
                _eventDispatcher.Dispatch(new FeatureOverrideHasBeenCreated(featureOverride.FeatureOverrideId, featureOverride.FeatureId, featureOverride.Hostname, featureOverride.IsEnabled));

                return HttpStatusCode.OK;
            }
            catch (CreateFeatureOverrideException exception)
            {
                _eventDispatcher.Dispatch(new FeatureErrorHasOccurred(exception.Message));
                return HttpStatusCode.BadRequest;
            }
        }

        private HttpStatusCode UpdateFeatureOverride()
        {
            try
            {
                var featureOverride = this.Bind<FeatureOverride>();
                _updateFeatureOverride.Execute(featureOverride.ToEntity());
                _eventDispatcher.Dispatch(new FeatureOverrideHasBeenUpdated(featureOverride.FeatureOverrideId, featureOverride.FeatureId, featureOverride.Hostname, featureOverride.IsEnabled));

                return HttpStatusCode.OK;
            }
            catch (UpdateFeatureOverrideException exception)
            {
                _eventDispatcher.Dispatch(new FeatureErrorHasOccurred(exception.Message));
                return HttpStatusCode.BadRequest;
            }
        }

        private HttpStatusCode DeleteFeatureOverride()
        {
            int featureOverrideId;
            int.TryParse(Request.Query["id"].Value as string, out featureOverrideId);

            try
            {
                _deleteFeatureOverride.Execute(featureOverrideId);
                _eventDispatcher.Dispatch(new FeatureOverrideHasBeenDeleted(featureOverrideId));
                return HttpStatusCode.OK;
            }
            catch (DeleteFeatureOverrideException exception)
            {
                _eventDispatcher.Dispatch(new FeatureErrorHasOccurred(exception.Message));
                return HttpStatusCode.BadRequest;
            }
        }

        private readonly IDeleteFeatureOverride _deleteFeatureOverride;
        private readonly IDomainEventDispatcher _eventDispatcher;
        private readonly ICreateFeatureOverride _createFeatureOverride;
        private readonly IUpdateFeatureOverride _updateFeatureOverride;
    }
}