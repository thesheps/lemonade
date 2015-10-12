using Lemonade.Data.Entities;

namespace Lemonade.Data.Commands
{
    public interface ICreateFeature
    {
        void Execute(Feature feature);
    }
}