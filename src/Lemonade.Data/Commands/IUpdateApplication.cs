using Lemonade.Core.Domain;

namespace Lemonade.Core.Commands
{
    public interface IUpdateApplication
    {
        void Execute(Application application);
    }
}