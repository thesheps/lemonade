using Lemonade.Data.Commands;
using Lemonade.Data.Exceptions;
using Lemonade.Web.Core.Commands;
using Lemonade.Web.Core.Events;

namespace Lemonade.Web.Core.CommandHandlers
{
    public class DeleteApplicationCommandHandler : ICommandHandler<DeleteApplicationCommand>
    {
        public DeleteApplicationCommandHandler(IDomainEventDispatcher eventDispatcher, IDeleteApplication deleteApplication)
        {
            _eventDispatcher = eventDispatcher;
            _deleteApplication = deleteApplication;
        }

        public void Handle(DeleteApplicationCommand command)
        {
            try
            {
                _deleteApplication.Execute(command.ApplicationId);
                _eventDispatcher.Dispatch(new ApplicationHasBeenDeleted(command.ApplicationId));
            }
            catch (DeleteApplicationException exception)
            {
                _eventDispatcher.Dispatch(new ApplicationErrorHasOccurred(exception.Message));
                throw;
            }
        }

        private readonly IDomainEventDispatcher _eventDispatcher;
        private readonly IDeleteApplication _deleteApplication;
    }
}