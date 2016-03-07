using Lemonade.Data.Commands;
using Lemonade.Data.Entities;
using Lemonade.Data.Exceptions;
using Lemonade.Web.Core.Commands;
using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Core.CommandHandlers
{
    public class CreateResourceCommandHandler : ICommandHandler<CreateResourceCommand>
    {
        public CreateResourceCommandHandler(IDomainEventDispatcher eventDispatcher, ICreateResource createResource)
        {
            _eventDispatcher = eventDispatcher;
            _createResource = createResource;
        }

        public void Handle(CreateResourceCommand command)
        {
            try
            {
                var resource = new Resource { ApplicationId = command.ApplicationId, LocaleId = command.LocaleId, ResourceKey = command.ResourceKey, ResourceSet = command.ResourceSet, Value = command.Value };
                _createResource.Execute(resource);
                _eventDispatcher.Dispatch(new ResourceHasBeenCreated(resource.ResourceId, resource.ApplicationId, resource.LocaleId, resource.ResourceSet, resource.ResourceKey, resource.Value));
            }
            catch (CreateResourceException exception)
            {
                _eventDispatcher.Dispatch(new ResourceErrorHasOccurred(exception.Message));
                throw;
            }
        }

        private readonly IDomainEventDispatcher _eventDispatcher;
        private readonly ICreateResource _createResource;
    }
}