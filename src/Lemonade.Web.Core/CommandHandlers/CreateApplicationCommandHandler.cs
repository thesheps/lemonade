﻿using Lemonade.Data.Commands;
using Lemonade.Data.Entities;
using Lemonade.Data.Exceptions;
using Lemonade.Web.Core.Commands;
using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Core.CommandHandlers
{
    public class CreateApplicationCommandHandler : ICommandHandler<CreateApplicationCommand>
    {
        public CreateApplicationCommandHandler(IDomainEventDispatcher eventDispatcher, ICreateApplication createApplication)
        {
            _eventDispatcher = eventDispatcher;
            _createApplication = createApplication;
        }

        public void Handle(CreateApplicationCommand command)
        {
            var application = new Application { Name = command.Name };

            try
            {
                _createApplication.Execute(application);
                _eventDispatcher.Dispatch(new ApplicationHasBeenCreated(application.ApplicationId, application.Name));
            }
            catch(CreateApplicationException ex)
            {
                _eventDispatcher.Dispatch(new ApplicationErrorHasOccurred(ex.Message));
                throw;
            }
        }

        private readonly IDomainEventDispatcher _eventDispatcher;
        private readonly ICreateApplication _createApplication;
    }
}