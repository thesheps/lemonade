using Lemonade.Data.Commands;
using Lemonade.Data.Entities;
using Lemonade.Data.Exceptions;
using Lemonade.Web.Core.Commands;
using Lemonade.Web.Core.Events;

namespace Lemonade.Web.Core.CommandHandlers
{
    public class UpdateApplicationCommandHandler : ICommandHandler<UpdateApplicationCommand>
    {
        public UpdateApplicationCommandHandler(IDomainEventDispatcher eventDispatcher, IUpdateApplication updateApplication)
        {
            _eventDispatcher = eventDispatcher;
            _updateApplication = updateApplication;
        }

        public void Handle(UpdateApplicationCommand command)
        {
            var application = new Application { Name = command.Name, ApplicationId = command.ApplicationId };

            try
            {
                _updateApplication.Execute(application);
                _eventDispatcher.Dispatch(new ApplicationHasBeenUpdated(application.ApplicationId, application.Name));
            }
            catch (UpdateApplicationException ex)
            {
                _eventDispatcher.Dispatch(new ApplicationErrorHasOccurred(ex.Message));
                throw;
            }
        }

        private readonly IDomainEventDispatcher _eventDispatcher;
        private readonly IUpdateApplication _updateApplication;
    }
}