using System.Collections.Generic;
using Lemonade.Data.Entities;

namespace Lemonade.Data.Queries
{
    public interface IGetAllResourcesByApplicationIdAndLocaleId
    {
        IList<Resource> Execute(int applicationId, int localeId);
    }
}