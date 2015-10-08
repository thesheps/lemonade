using System.Collections.Generic;
using Lemonade.Core.Domain;

namespace Lemonade.Core.Queries
{
    public interface IGetAllFeaturesByApplicationId
    {
        IList<Feature> Execute(int applicationId);
    }
}