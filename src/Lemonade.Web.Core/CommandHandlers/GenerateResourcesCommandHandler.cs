using Lemonade.Data.Queries;
using Lemonade.Web.Core.Commands;
using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Exceptions;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Core.CommandHandlers
{
    public class GenerateResourcesCommandHandler : ICommandHandler<GenerateResourcesCommand>
    {
        public GenerateResourcesCommandHandler(IDomainEventDispatcher eventDispatcher, ICommandDispatcher commandDispatcher, ITranslateResourceFactory translateResourceFactory, IGetLocaleById getLocaleById, IGetAllResourcesByApplicationIdAndLocaleId getAllResourcesByApplicationIdAndLocaleId)
        {
            _eventDispatcher = eventDispatcher;
            _commandDispatcher = commandDispatcher;
            _translateResourceFactory = translateResourceFactory;
            _getLocaleById = getLocaleById;
            _getAllResourcesByApplicationIdAndLocaleId = getAllResourcesByApplicationIdAndLocaleId;
        }

        public void Handle(GenerateResourcesCommand command)
        {
            try
            {
                var resources = _getAllResourcesByApplicationIdAndLocaleId.Execute(command.ApplicationId, command.LocaleId);
                var translateResource = _translateResourceFactory.Create(command.TranslationType);
                var targetLocale = _getLocaleById.Execute(command.TargetLocaleId);

                foreach (var resource in resources)
                {
                    var value = translateResource.Translate(resource.Value, resource.Locale.IsoCode, targetLocale.IsoCode);
                    _commandDispatcher.Dispatch(new CreateResourceCommand(command.ApplicationId, targetLocale.LocaleId, resource.ResourceKey, resource.ResourceSet, value));
                }
            }
            catch (UnsupportedTranslationException exception)
            {
                _eventDispatcher.Dispatch(new ResourceGenerationErrorHasOccurred(exception.Message));
                throw;
            }
        }

        private readonly IDomainEventDispatcher _eventDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly ITranslateResourceFactory _translateResourceFactory;
        private readonly IGetLocaleById _getLocaleById;
        private readonly IGetAllResourcesByApplicationIdAndLocaleId _getAllResourcesByApplicationIdAndLocaleId;
    }
}