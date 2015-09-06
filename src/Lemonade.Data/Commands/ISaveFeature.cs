namespace Lemonade.Data.Commands
{
    public interface ISaveFeature
    {
        void Execute(Entities.Feature feature);
    }
}