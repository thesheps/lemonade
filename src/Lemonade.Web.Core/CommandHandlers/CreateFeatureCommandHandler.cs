using Lemonade.Data.Commands;
using Lemonade.Data.Entities;
using Lemonade.Data.Exceptions;
using Lemonade.Web.Core.Commands;
using Lemonade.Web.Core.Events;

namespace Lemonade.Web.Core.CommandHandlers
{
    public class CreateFeatureCommandHandler : ICommandHandler<CreateFeatureCommand>
    {
        public CreateFeatureCommandHandler(IDomainEventDispatcher eventDispatcher, ICreateFeature createFeature)
        {
            _eventDispatcher = eventDispatcher;
            _createFeature = createFeature;
        }

        public void Handle(CreateFeatureCommand command)
        {
            try
            {
                var feature = new Feature { ApplicationId = command.ApplicationId, Name = command.Name, IsEnabled = command.IsEnabled };
                _createFeature.Execute(feature);
                _eventDispatcher.Dispatch(new FeatureHasBeenCreated(feature.FeatureId, feature.ApplicationId, feature.Name, feature.IsEnabled));
            }
            catch (CreateFeatureException exception)
            {
                _eventDispatcher.Dispatch(new FeatureErrorHasOccurred(exception.Message));
                throw;
            }
        }

        private readonly IDomainEventDispatcher _eventDispatcher;
        private readonly ICreateFeature _createFeature;
    }
}