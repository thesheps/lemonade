using Lemonade.Data.Entities;

namespace Lemonade.Data.Commands
{
    public interface ICreateLocale
    {
        void Execute(Locale locale);
    }
}