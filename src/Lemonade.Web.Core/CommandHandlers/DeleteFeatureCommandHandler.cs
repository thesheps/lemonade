using Lemonade.Data.Commands;
using Lemonade.Data.Exceptions;
using Lemonade.Web.Core.Commands;
using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Core.CommandHandlers
{
    public class DeleteFeatureCommandHandler : ICommandHandler<DeleteFeatureCommand>
    {
        public DeleteFeatureCommandHandler(IDomainEventDispatcher eventDispatcher, IDeleteFeature deleteFeature)
        {
            _eventDispatcher = eventDispatcher;
            _deleteFeature = deleteFeature;
        }

        public void Handle(DeleteFeatureCommand command)
        {
            try
            {
                _deleteFeature.Execute(command.FeatureId);
                _eventDispatcher.Dispatch(new FeatureHasBeenDeleted(command.FeatureId));
            }
            catch (DeleteFeatureException exception)
            {
                _eventDispatcher.Dispatch(new FeatureErrorHasOccurred(exception.Message));
            }
        }

        private readonly IDomainEventDispatcher _eventDispatcher;
        private readonly IDeleteFeature _deleteFeature;
    }
}