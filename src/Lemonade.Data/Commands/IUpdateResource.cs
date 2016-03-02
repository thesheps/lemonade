using Lemonade.Data.Entities;

namespace Lemonade.Data.Commands
{
    public interface IUpdateResource
    {
        void Execute(Resource resource);
    }
}