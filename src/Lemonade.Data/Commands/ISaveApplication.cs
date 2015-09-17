using Lemonade.Data.Entities;

namespace Lemonade.Data.Commands
{
    public interface ISaveApplication
    {
        void Execute(Application application);
    }
}