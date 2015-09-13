using Lemonade.Data.Entities;

namespace Lemonade.Data.Commands
{
    public interface ISaveFeature
    {
        void Execute(Feature feature);
    }
}