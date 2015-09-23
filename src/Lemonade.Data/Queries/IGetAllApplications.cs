using System.Collections.Generic;
using Lemonade.Core.Entities;

namespace Lemonade.Core.Queries
{
    public interface IGetAllApplications
    {
        IList<Application> Execute();
    }
}