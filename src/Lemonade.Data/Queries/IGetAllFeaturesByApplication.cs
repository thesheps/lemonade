using System.Collections.Generic;

namespace Lemonade.Data.Queries
{
    public interface IGetAllFeaturesByApplication
    {
        IEnumerable<Entities.Feature> Execute(string application);
    }
}