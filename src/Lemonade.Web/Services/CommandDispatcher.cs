using System.Linq;
using Lemonade.Web.Core.Commands;
using Nancy.TinyIoc;

namespace Lemonade.Web.Services
{
    public class CommandDispatcher : ICommandDispatcher
    {
        public CommandDispatcher(TinyIoCContainer container)
        {
            _container = container;
        }

        public void Dispatch<TCommand>(TCommand command) where TCommand : ICommand
        {
            _container.ResolveAll<ICommandHandler<TCommand>>().ToList().ForEach(h => h.Handle(command));
        }

        private readonly TinyIoCContainer _container;
    }
}