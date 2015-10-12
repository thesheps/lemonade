using Lemonade.Data.Entities;

namespace Lemonade.Data.Commands
{
    public interface ICreateApplication
    {
        void Execute(Application application);
    }
}