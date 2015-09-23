using Lemonade.Core.Entities;

namespace Lemonade.Core.Commands
{
    public interface ISaveApplication
    {
        void Execute(Application application);
    }
}