using System.Collections.Generic;
using Lemonade.Core.Domain;

namespace Lemonade.Core.Queries
{
    public interface IGetAllFeatures
    {
        IList<Feature> Execute();
    }
}