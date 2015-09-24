using System.Collections.Generic;
using Lemonade.Core.Domain;

namespace Lemonade.Core.Queries
{
    public interface IGetAllApplications
    {
        IList<Application> Execute();
    }
}