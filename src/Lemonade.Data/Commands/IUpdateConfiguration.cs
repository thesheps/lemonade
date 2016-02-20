using Lemonade.Data.Entities;

namespace Lemonade.Data.Commands
{
    public interface IUpdateConfiguration
    {
        void Execute(Configuration configuration);
    }
}