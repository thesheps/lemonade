using System.Collections.Generic;
using Lemonade.Data.Entities;

namespace Lemonade.Data.Queries
{
    public interface IGetAllResourcesByApplicationId
    {
        IList<Resource> Execute(int applicationId);
    }
}