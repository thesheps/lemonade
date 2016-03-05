using Lemonade.Web.Core.CommandHandlers;
using Lemonade.Web.Core.Commands;
using Lemonade.Web.Core.EventHandlers;
using Lemonade.Web.Core.Events;
using Nancy.TinyIoc;

namespace Lemonade.Web.Infrastructure
{
    public static class Installer
    {
        public static TinyIoCContainer InstallDomainEventHandlers(this TinyIoCContainer container)
        {
            container.Register<IDomainEventHandler<ConfigurationHasBeenCreated>, ConfigurationHasBeenCreatedHandler>();
            container.Register<IDomainEventHandler<ConfigurationHasBeenUpdated>, ConfigurationHasBeenUpdatedHandler>();
            container.Register<IDomainEventHandler<ConfigurationHasBeenDeleted>, ConfigurationHasBeenDeletedHandler>();
            container.Register<IDomainEventHandler<ConfigurationErrorHasOccurred>, ConfigurationErrorHasOccurredHandler>();
            container.Register<IDomainEventHandler<ApplicationHasBeenCreated>, ApplicationHasBeenCreatedHandler>();
            container.Register<IDomainEventHandler<ApplicationHasBeenUpdated>, ApplicationHasBeenUpdatedHandler>();
            container.Register<IDomainEventHandler<ApplicationHasBeenDeleted>, ApplicationHasBeenDeletedHandler>();
            container.Register<IDomainEventHandler<ApplicationErrorHasOccurred>, ApplicationErrorHasOccurredHandler>();
            container.Register<IDomainEventHandler<FeatureHasBeenCreated>, FeatureHasBeenCreatedHandler>();
            container.Register<IDomainEventHandler<FeatureHasBeenUpdated>, FeatureHasBeenUpdatedHandler>();
            container.Register<IDomainEventHandler<FeatureHasBeenDeleted>, FeatureHasBeenDeletedHandler>();
            container.Register<IDomainEventHandler<FeatureErrorHasOccurred>, FeatureErrorHasOccurredHandler>();
            container.Register<IDomainEventHandler<FeatureOverrideHasBeenCreated>, FeatureOverrideHasBeenCreatedHandler>();
            container.Register<IDomainEventHandler<FeatureOverrideHasBeenUpdated>, FeatureOverrideHasBeenUpdatedHandler>();
            container.Register<IDomainEventHandler<FeatureOverrideHasBeenDeleted>, FeatureOverrideHasBeenDeletedHandler>();
            container.Register<IDomainEventHandler<FeatureOverrideErrorHasOccurred>, FeatureOverrideErrorHasOccurredHandler>();
            container.Register<IDomainEventHandler<ResourceHasBeenCreated>, ResourceHasBeenCreatedHandler>();
            container.Register<IDomainEventHandler<ResourceHasBeenUpdated>, ResourceHasBeenUpdatedHandler>();
            container.Register<IDomainEventHandler<ResourceHasBeenDeleted>, ResourceHasBeenDeletedHandler>();
            container.Register<IDomainEventHandler<ResourceErrorHasOccurred>, ResourceErrorHasOccurredHandler>();

            return container;
        }

        public static TinyIoCContainer InstallCommandHandlers(this TinyIoCContainer container)
        {
            container.Register<ICommandHandler<CreateApplicationCommand>, CreateApplicationCommandHandler>();
            container.Register<ICommandHandler<UpdateApplicationCommand>, UpdateApplicationCommandHandler>();
            container.Register<ICommandHandler<DeleteApplicationCommand>, DeleteApplicationCommandHandler>();
            container.Register<ICommandHandler<CreateConfigurationCommand>, CreateConfigurationCommandHandler>();
            container.Register<ICommandHandler<UpdateConfigurationCommand>, UpdateConfigurationCommandHandler>();
            container.Register<ICommandHandler<DeleteConfigurationCommand>, DeleteConfigurationCommandHandler>();

            return container;
        }
    }
}