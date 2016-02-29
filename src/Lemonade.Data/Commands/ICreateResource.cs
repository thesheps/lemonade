using Lemonade.Data.Entities;

namespace Lemonade.Data.Commands
{
    public interface ICreateResource
    {
        void Execute(Resource resource);
    }
}