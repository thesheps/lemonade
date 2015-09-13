using System.Collections.Generic;
using Lemonade.Data.Entities;

namespace Lemonade.Data.Queries
{
    public interface IGetAllFeatures
    {
        IEnumerable<Feature> Execute();
    }
}