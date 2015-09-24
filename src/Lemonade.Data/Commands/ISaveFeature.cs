using Lemonade.Core.Domain;

namespace Lemonade.Core.Commands
{
    public interface ISaveFeature
    {
        void Execute(Feature feature);
    }
}