using Lemonade.Data.Entities;

namespace Lemonade.Data.Commands
{
    public interface IUpdateApplication
    {
        void Execute(Application application);
    }
}