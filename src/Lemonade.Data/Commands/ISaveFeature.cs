using Lemonade.Core.Entities;

namespace Lemonade.Core.Commands
{
    public interface ISaveFeature
    {
        void Execute(Feature feature);
    }
}