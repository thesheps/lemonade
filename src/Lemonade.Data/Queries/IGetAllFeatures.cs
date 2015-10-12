using System.Collections.Generic;
using Lemonade.Data.Entities;

namespace Lemonade.Data.Queries
{
    public interface IGetAllFeatures
    {
        IList<Feature> Execute();
    }
}