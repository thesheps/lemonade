using System.Collections.Generic;
using Lemonade.Data.Entities;

namespace Lemonade.Data.Queries
{
    public interface IGetAllFeaturesByApplicationId
    {
        IList<Feature> Execute(int applicationId);
    }
}