using Lemonade.Data.Commands;
using Lemonade.Data.Entities;
using Lemonade.Data.Exceptions;
using Lemonade.Web.Core.Commands;
using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Core.CommandHandlers
{
    public class UpdateFeatureCommandHandler : ICommandHandler<UpdateFeatureCommand>
    {
        public UpdateFeatureCommandHandler(IDomainEventDispatcher eventDispatcher, IUpdateFeature updateFeature)
        {
            _eventDispatcher = eventDispatcher;
            _updateFeature = updateFeature;
        }

        public void Handle(UpdateFeatureCommand command)
        {
            try
            {
                var feature = new Feature { FeatureId = command.FeatureId, Name = command.Name, IsEnabled = command.IsEnabled };
                _updateFeature.Execute(feature);
                _eventDispatcher.Dispatch(new FeatureHasBeenUpdated(feature.FeatureId, feature.ApplicationId, feature.Name, feature.IsEnabled));
            }
            catch (UpdateFeatureException exception)
            {
                _eventDispatcher.Dispatch(new FeatureErrorHasOccurred(exception.Message));
            }
        }

        private readonly IDomainEventDispatcher _eventDispatcher;
        private readonly IUpdateFeature _updateFeature;
    }
}