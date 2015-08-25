using System.Collections.Generic;

namespace Lemonade.Data.Queries
{
    public interface IGetAllFeatures
    {
        IEnumerable<Entities.Feature> Execute();
    }
}