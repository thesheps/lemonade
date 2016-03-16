using Lemonade.Data.Entities;

namespace Lemonade.Data.Queries
{
    public interface IGetLocaleById
    {
        Locale Execute(int localeId);
    }
}