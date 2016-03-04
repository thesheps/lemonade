using Lemonade.Web.CommandHandlers;
using Lemonade.Web.Core.Commands;
using Nancy.TinyIoc;

namespace Lemonade.Web.Infrastructure
{
    public static class CommandInstaller
    {
        public static void Install(TinyIoCContainer container)
        {
            container.Register<ICommandHandler<CreateApplicationCommand>, CreateApplicationCommandHandler>();
        }
    }
}