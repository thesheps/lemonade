using Lemonade.Core.Domain;

namespace Lemonade.Core.Commands
{
    public interface ICreateApplication
    {
        void Execute(Application application);
    }
}