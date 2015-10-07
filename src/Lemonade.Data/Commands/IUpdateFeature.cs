using Lemonade.Core.Domain;

namespace Lemonade.Core.Commands
{
    public interface IUpdateFeature
    {
        void Execute(Feature feature);
    }
}