using Lemonade.Data.Entities;

namespace Lemonade.Data.Commands
{
    public interface IUpdateFeature
    {
        void Execute(Feature feature);
    }
}