using Lemonade.Core.Domain;

namespace Lemonade.Core.Commands
{
    public interface ISaveApplication
    {
        void Execute(Application application);
    }
}