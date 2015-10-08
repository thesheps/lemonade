using Lemonade.Core.Domain;

namespace Lemonade.Core.Commands
{
    public interface ICreateFeature
    {
        void Execute(Feature feature);
    }
}