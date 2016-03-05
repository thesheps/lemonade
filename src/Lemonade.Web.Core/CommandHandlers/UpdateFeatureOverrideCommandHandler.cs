using Lemonade.Data.Entities;
using Lemonade.Data.Exceptions;
using Lemonade.Data.Queries;
using Lemonade.Web.Core.Commands;
using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Core.CommandHandlers
{
    public class UpdateFeatureOverrideCommandHandler : ICommandHandler<UpdateFeatureOverrideCommand>
    {
        public UpdateFeatureOverrideCommandHandler(IDomainEventDispatcher eventDispatcher, IUpdateFeatureOverride updateFeatureOverride)
        {
            _eventDispatcher = eventDispatcher;
            _updateFeatureOverride = updateFeatureOverride;
        }

        public void Handle(UpdateFeatureOverrideCommand command)
        {
            try
            {
                _updateFeatureOverride.Execute(new FeatureOverride { FeatureId = command.FeatureId, FeatureOverrideId = command.FeatureOverrideId, Hostname = command.Hostname, IsEnabled = command.IsEnabled});
                _eventDispatcher.Dispatch(new FeatureOverrideHasBeenUpdated(command.FeatureOverrideId, command.FeatureId, command.Hostname, command.IsEnabled));
            }
            catch (UpdateFeatureOverrideException exception)
            {
                _eventDispatcher.Dispatch(new FeatureErrorHasOccurred(exception.Message));
            }
        }

        private readonly IDomainEventDispatcher _eventDispatcher;
        private readonly IUpdateFeatureOverride _updateFeatureOverride;
    }
}