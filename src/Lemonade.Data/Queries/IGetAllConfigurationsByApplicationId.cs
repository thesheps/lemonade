using System.Collections.Generic;
using Lemonade.Data.Entities;

namespace Lemonade.Data.Queries
{
    public interface IGetAllConfigurationsByApplicationId
    {
        IList<Configuration> Execute(int applicationId);
    }
}