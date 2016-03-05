using Lemonade.Data.Commands;
using Lemonade.Data.Exceptions;
using Lemonade.Web.Core.Commands;
using Lemonade.Web.Core.Events;

namespace Lemonade.Web.Core.CommandHandlers
{
    public class DeleteResourceCommandHandler : ICommandHandler<DeleteResourceCommand>
    {
        public DeleteResourceCommandHandler(IDomainEventDispatcher eventDispatcher, IDeleteResource deleteResource)
        {
            _eventDispatcher = eventDispatcher;
            _deleteResource = deleteResource;
        }

        public void Handle(DeleteResourceCommand command)
        {
            try
            {
                _deleteResource.Execute(command.ResourceId);
                _eventDispatcher.Dispatch(new ResourceHasBeenDeleted(command.ResourceId));
            }
            catch (DeleteResourceException exception)
            {
                _eventDispatcher.Dispatch(new ResourceErrorHasOccurred(exception.Message));
            }
        }

        private readonly IDomainEventDispatcher _eventDispatcher;
        private readonly IDeleteResource _deleteResource;
    }
}