using System.Collections.Generic;
using Lemonade.Core.Entities;

namespace Lemonade.Core.Queries
{
    public interface IGetAllFeatures
    {
        IList<Feature> Execute();
    }
}