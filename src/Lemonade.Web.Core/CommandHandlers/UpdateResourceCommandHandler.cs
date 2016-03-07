using Lemonade.Data.Commands;
using Lemonade.Data.Entities;
using Lemonade.Data.Exceptions;
using Lemonade.Web.Core.Commands;
using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Core.CommandHandlers
{
    public class UpdateResourceCommandHandler : ICommandHandler<UpdateResourceCommand>
    {
        public UpdateResourceCommandHandler(IDomainEventDispatcher eventDispatcher, IUpdateResource updateResource)
        {
            _eventDispatcher = eventDispatcher;
            _updateResource = updateResource;
        }

        public void Handle(UpdateResourceCommand command)
        {
            try
            {
                var resource = new Resource { ResourceId = command.ResourceId, LocaleId = command.LocaleId, ResourceKey = command.ResourceKey, ResourceSet = command.ResourceSet, Value = command.Value };
                _updateResource.Execute(resource);
                _eventDispatcher.Dispatch(new ResourceHasBeenUpdated(resource.ResourceId, resource.ApplicationId, resource.LocaleId, resource.ResourceSet, resource.ResourceKey, resource.Value));
            }
            catch (UpdateResourceException exception)
            {
                _eventDispatcher.Dispatch(new ResourceErrorHasOccurred(exception.Message));
                throw;
            }
        }

        private readonly IDomainEventDispatcher _eventDispatcher;
        private readonly IUpdateResource _updateResource;
    }
}