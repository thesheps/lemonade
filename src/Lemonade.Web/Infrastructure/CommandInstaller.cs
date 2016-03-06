using Lemonade.Web.Core.CommandHandlers;
using Lemonade.Web.Core.Commands;
using Nancy.TinyIoc;

namespace Lemonade.Web.Infrastructure
{
    public static class CommandInstaller
    {
        public static TinyIoCContainer InstallCommandHandlers(this TinyIoCContainer container)
        {
            container.Register<ICommandHandler<CreateApplicationCommand>, CreateApplicationCommandHandler>();
            container.Register<ICommandHandler<UpdateApplicationCommand>, UpdateApplicationCommandHandler>();
            container.Register<ICommandHandler<DeleteApplicationCommand>, DeleteApplicationCommandHandler>();
            container.Register<ICommandHandler<CreateConfigurationCommand>, CreateConfigurationCommandHandler>();
            container.Register<ICommandHandler<UpdateConfigurationCommand>, UpdateConfigurationCommandHandler>();
            container.Register<ICommandHandler<DeleteConfigurationCommand>, DeleteConfigurationCommandHandler>();
            container.Register<ICommandHandler<CreateResourceCommand>, CreateResourceCommandHandler>();
            container.Register<ICommandHandler<UpdateResourceCommand>, UpdateResourceCommandHandler>();
            container.Register<ICommandHandler<DeleteResourceCommand>, DeleteResourceCommandHandler>();
            container.Register<ICommandHandler<CreateFeatureCommand>, CreateFeatureCommandHandler>();
            container.Register<ICommandHandler<UpdateFeatureCommand>, UpdateFeatureCommandHandler>();
            container.Register<ICommandHandler<DeleteFeatureCommand>, DeleteFeatureCommandHandler>();
            container.Register<ICommandHandler<CreateFeatureOverrideCommand>, CreateFeatureOverrideCommandHandler>();
            container.Register<ICommandHandler<UpdateFeatureOverrideCommand>, UpdateFeatureOverrideCommandHandler>();
            container.Register<ICommandHandler<DeleteFeatureOverrideCommand>, DeleteFeatureOverrideCommandHandler>();

            return container;
        }
    }
}