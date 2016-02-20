namespace Lemonade.Data.Commands
{
    public interface ICreateConfiguration
    {
        void Execute(Entities.Configuration configuration);
    }
}