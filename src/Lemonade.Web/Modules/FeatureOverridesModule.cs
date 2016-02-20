using Lemonade.Data.Commands;
using Lemonade.Data.Exceptions;
using Lemonade.Data.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Events;
using Lemonade.Web.Mappers;
using Nancy;
using Nancy.ModelBinding;

namespace Lemonade.Web.Modules
{
    public class FeatureOverridesModule : NancyModule
    {
        public FeatureOverridesModule(ICreateFeatureOverride createFeatureOverride, IUpdateFeatureOverride updateFeatureOverride, IDeleteFeatureOverride deleteFeatureOverride)
        {
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
                DomainEvents.Raise(new FeatureOverrideHasBeenCreated(featureOverride.FeatureOverrideId, featureOverride.FeatureId, featureOverride.Hostname, featureOverride.IsEnabled));

                return HttpStatusCode.OK;
            }
            catch (CreateFeatureOverrideException exception)
            {
                DomainEvents.Raise(new FeatureErrorHasOccurred(exception.Message));
                return HttpStatusCode.BadRequest;
            }
        }

        private HttpStatusCode UpdateFeatureOverride()
        {
            try
            {
                var featureOverride = this.Bind<FeatureOverride>();
                _updateFeatureOverride.Execute(featureOverride.ToEntity());
                DomainEvents.Raise(new FeatureOverrideHasBeenUpdated(featureOverride.FeatureOverrideId, featureOverride.FeatureId, featureOverride.Hostname, featureOverride.IsEnabled));

                return HttpStatusCode.OK;
            }
            catch (UpdateFeatureOverrideException exception)
            {
                DomainEvents.Raise(new FeatureErrorHasOccurred(exception.Message));
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
                DomainEvents.Raise(new FeatureOverrideHasBeenDeleted(featureOverrideId));
                return HttpStatusCode.OK;
            }
            catch (DeleteFeatureOverrideException exception)
            {
                DomainEvents.Raise(new FeatureErrorHasOccurred(exception.Message));
                return HttpStatusCode.BadRequest;
            }
        }

        private readonly IDeleteFeatureOverride _deleteFeatureOverride;
        private readonly ICreateFeatureOverride _createFeatureOverride;
        private readonly IUpdateFeatureOverride _updateFeatureOverride;
    }
}