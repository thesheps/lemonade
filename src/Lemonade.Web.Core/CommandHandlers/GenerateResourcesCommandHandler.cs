using System.Data.Common;
using Lemonade.Data.Commands;
using Lemonade.Data.Entities;
using Lemonade.Data.Exceptions;
using Lemonade.Data.Queries;
using Lemonade.Web.Core.Commands;
using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Exceptions;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Core.CommandHandlers
{
    public class GenerateResourcesCommandHandler : ICommandHandler<GenerateResourcesCommand>
    {
        public GenerateResourcesCommandHandler(IDomainEventDispatcher eventDispatcher, ITranslateResourceFactory translateResourceFactory, IGetLocaleById getLocaleById, IGetAllResourcesByApplicationIdAndLocaleId getAllResourcesByApplicationIdAndLocaleId, ICreateResource createResource)
        {
            _eventDispatcher = eventDispatcher;
            _translateResourceFactory = translateResourceFactory;
            _getLocaleById = getLocaleById;
            _getAllResourcesByApplicationIdAndLocaleId = getAllResourcesByApplicationIdAndLocaleId;
            _createResource = createResource;
        }

        public void Handle(GenerateResourcesCommand command)
        {
            try
            {
                var resources = _getAllResourcesByApplicationIdAndLocaleId.Execute(command.ApplicationId,
                    command.LocaleId);
                var translateResource = _translateResourceFactory.Create(command.Type);
                var targetLocale = _getLocaleById.Execute(command.TargetLocaleId);

                foreach (var resource in resources)
                {
                    var value = translateResource.Translate(resource.Value, resource.Locale.IsoCode,
                        targetLocale.IsoCode);
                    _createResource.Execute(new Resource
                    {
                        ApplicationId = command.ApplicationId,
                        LocaleId = command.TargetLocaleId,
                        ResourceKey = resource.ResourceKey,
                        ResourceSet = resource.ResourceSet,
                        Value = value
                    });
                }
            }
            catch (UnsupportedTranslationException exception)
            {
                _eventDispatcher.Dispatch(new ResourceGenerationErrorHasOccurred(exception.Message));
                throw;
            }
            catch (CreateResourceException exception)
            {
                _eventDispatcher.Dispatch(new ResourceGenerationErrorHasOccurred(exception.Message));
                throw;
            }
        }

        private readonly IDomainEventDispatcher _eventDispatcher;
        private readonly ITranslateResourceFactory _translateResourceFactory;
        private readonly IGetLocaleById _getLocaleById;
        private readonly IGetAllResourcesByApplicationIdAndLocaleId _getAllResourcesByApplicationIdAndLocaleId;
        private readonly ICreateResource _createResource;
    }
}