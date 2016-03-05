using Lemonade.Data.Commands;
using Lemonade.Data.Exceptions;
using Lemonade.Web.Core.Commands;
using Lemonade.Web.Core.Events;

namespace Lemonade.Web.Core.CommandHandlers
{
    public class DeleteFeatureOverrideCommandHandler : ICommandHandler<DeleteFeatureOverrideCommand>
    {
        public DeleteFeatureOverrideCommandHandler(IDomainEventDispatcher eventDispatcher, IDeleteFeatureOverride deleteFeatureOverride)
        {
            _eventDispatcher = eventDispatcher;
            _deleteFeatureOverride = deleteFeatureOverride;
        }

        public void Handle(DeleteFeatureOverrideCommand command)
        {
            try
            {
                _deleteFeatureOverride.Execute(command.FeatureOverrideId);
                _eventDispatcher.Dispatch(new FeatureOverrideHasBeenDeleted(command.FeatureOverrideId));
            }
            catch (DeleteFeatureOverrideException exception)
            {
                _eventDispatcher.Dispatch(new FeatureErrorHasOccurred(exception.Message));
                throw;
            }
        }

        private readonly IDomainEventDispatcher _eventDispatcher;
        private readonly IDeleteFeatureOverride _deleteFeatureOverride;
    }
}