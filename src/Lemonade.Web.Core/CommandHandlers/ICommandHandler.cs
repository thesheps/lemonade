using Lemonade.Web.Core.Commands;

namespace Lemonade.Web.Core.CommandHandlers
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        void Handle(TCommand command);
    }
}