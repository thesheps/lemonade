using System.Collections.Generic;
using Lemonade.Data.Entities;

namespace Lemonade.Data.Queries
{
    public interface IGetAllFeaturesByApplication
    {
        IEnumerable<Feature> Execute(string applicationName);
    }
}