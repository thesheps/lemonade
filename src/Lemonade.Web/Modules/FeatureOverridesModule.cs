using Lemonade.Data.Exceptions;
using Lemonade.Web.Contracts;
using Lemonade.Web.Core.Commands;
using Lemonade.Web.Core.Events;
using Lemonade.Web.Mappers;
using Nancy;
using Nancy.ModelBinding;

namespace Lemonade.Web.Modules
{
    public class FeatureOverridesModule : NancyModule
    {
        public FeatureOverridesModule(IDomainEventDispatcher eventDispatcher, ICommandDispatcher commandDispatcher)
        {
            _eventDispatcher = eventDispatcher;
            _commandDispatcher = commandDispatcher;
            Post["/api/featureoverrides"] = p => CreateFeatureOverride();
            Put["/api/featureoverrides"] = p => UpdateFeatureOverride();
            Delete["/api/featureoverrides"] = p => DeleteFeatureOverride();
        }

        private HttpStatusCode CreateFeatureOverride()
        {
            try
            {
                var featureOverride = this.Bind<FeatureOverride>();
                _commandDispatcher.Dispatch(new CreateFeatureOverrideCommand(featureOverride.FeatureId, featureOverride.FeatureOverrideId, featureOverride.Hostname, featureOverride.IsEnabled));
                return HttpStatusCode.OK;
            }
            catch (CreateFeatureOverrideException)
            {
                return HttpStatusCode.BadRequest;
            }
        }

        private HttpStatusCode UpdateFeatureOverride()
        {
            try
            {
                var featureOverride = this.Bind<FeatureOverride>();
                _commandDispatcher.Dispatch(new UpdateFeatureOverrideCommand(featureOverride.FeatureId, featureOverride.FeatureOverrideId, featureOverride.Hostname, featureOverride.IsEnabled));

                return HttpStatusCode.OK;
            }
            catch (UpdateFeatureOverrideException)
            {
                return HttpStatusCode.BadRequest;
            }
        }

        private HttpStatusCode DeleteFeatureOverride()
        {
            int featureOverrideId;
            int.TryParse(Request.Query["id"].Value as string, out featureOverrideId);

            try
            {
                _commandDispatcher.Dispatch(new DeleteFeatureOverrideCommand(featureOverrideId));                
                return HttpStatusCode.OK;
            }
            catch (DeleteFeatureOverrideException)
            {
                return HttpStatusCode.BadRequest;
            }
        }

        private readonly IDomainEventDispatcher _eventDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
    }
}