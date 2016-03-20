using System;
using System.Text.RegularExpressions;
using Lemonade.Web.Contracts;
using Lemonade.Web.Core.Commands;
using Lemonade.Web.Core.Services;
using Nancy;
using Nancy.ModelBinding;

namespace Lemonade.Web.Modules
{
    public class FeatureOverridesModule : NancyModule
    {
        public FeatureOverridesModule(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            Post["/api/featureoverrides"] = p => CreateFeatureOverride();
            Put["/api/featureoverrides"] = p => UpdateFeatureOverride();
            Delete["/api/featureoverrides"] = p => DeleteFeatureOverride();
        }

        private Response CreateFeatureOverride()
        {
            try
            {
                var featureOverride = this.Bind<FeatureOverride>();
                _commandDispatcher.Dispatch(new CreateFeatureOverrideCommand(featureOverride.FeatureId, featureOverride.FeatureOverrideId, featureOverride.Hostname, featureOverride.IsEnabled));
                return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                return new Response { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = Regex.Replace(ex.InnerException.Message, @"\t|\n|\r", "") };
            }
        }

        private Response UpdateFeatureOverride()
        {
            try
            {
                var featureOverride = this.Bind<FeatureOverride>();
                _commandDispatcher.Dispatch(new UpdateFeatureOverrideCommand(featureOverride.FeatureId, featureOverride.FeatureOverrideId, featureOverride.Hostname, featureOverride.IsEnabled));

                return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                return new Response { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = Regex.Replace(ex.InnerException.Message, @"\t|\n|\r", "") };
            }
        }

        private Response DeleteFeatureOverride()
        {
            int featureOverrideId;
            int.TryParse(Request.Query["id"].Value as string, out featureOverrideId);

            try
            {
                _commandDispatcher.Dispatch(new DeleteFeatureOverrideCommand(featureOverrideId));                
                return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                return new Response { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = Regex.Replace(ex.InnerException.Message, @"\t|\n|\r", "") };
            }
        }

        private readonly ICommandDispatcher _commandDispatcher;
    }
}