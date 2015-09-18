using System.Collections.Generic;
using Lemonade.Data.Entities;

namespace Lemonade.Data.Queries
{
    public interface IGetAllFeaturesByApplication
    {
        IList<Feature> Execute(string applicationName);
    }
}