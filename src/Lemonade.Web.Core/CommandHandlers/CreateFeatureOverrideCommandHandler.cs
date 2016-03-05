using Lemonade.Data.Commands;
using Lemonade.Data.Entities;
using Lemonade.Data.Exceptions;
using Lemonade.Web.Core.Commands;
using Lemonade.Web.Core.Events;

namespace Lemonade.Web.Core.CommandHandlers
{
    public class CreateFeatureOverrideCommandHandler : ICommandHandler<CreateFeatureOverrideCommand>
    {
        public CreateFeatureOverrideCommandHandler(IDomainEventDispatcher eventDispatcher, ICreateFeatureOverride createFeatureOverride)
        {
            _eventDispatcher = eventDispatcher;
            _createFeatureOverride = createFeatureOverride;
        }

        public void Handle(CreateFeatureOverrideCommand command)
        {
            try
            {
                var featureOverride = new FeatureOverride { FeatureId = command.FeatureId, FeatureOverrideId = command.FeatureOverrideId, Hostname = command.Hostname, IsEnabled = command.IsEnabled };
                _createFeatureOverride.Execute(featureOverride);
                _eventDispatcher.Dispatch(new FeatureOverrideHasBeenCreated(featureOverride.FeatureOverrideId, featureOverride.FeatureId, featureOverride.Hostname, featureOverride.IsEnabled));
            }
            catch (CreateFeatureOverrideException exception)
            {
                _eventDispatcher.Dispatch(new FeatureErrorHasOccurred(exception.Message));
                throw;
            }
        }

        private readonly IDomainEventDispatcher _eventDispatcher;
        private readonly ICreateFeatureOverride _createFeatureOverride;
    }
}