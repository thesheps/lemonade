namespace Lemonade.Web.Core.Commands
{
    public interface ICommandDispatcher
    {
        void Dispatch<TCommand>(TCommand command) where TCommand : ICommand;
    }
}