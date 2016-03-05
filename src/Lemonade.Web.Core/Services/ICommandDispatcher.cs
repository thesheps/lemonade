using Lemonade.Web.Core.Commands;

namespace Lemonade.Web.Core.Services
{
    public interface ICommandDispatcher
    {
        void Dispatch<TCommand>(TCommand command) where TCommand : ICommand;
    }
}