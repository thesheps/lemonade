using System.Data.Common;
using System.Linq;
using Dapper;
using Lemonade.Data.Commands;
using Lemonade.Data.Entities;
using Lemonade.Data.Exceptions;

namespace Lemonade.Sql.Commands
{
    public class CreateResource : LemonadeConnection, ICreateResource
    {
        public CreateResource()
        {
        }

        public CreateResource(string connectionStringName) : base(connectionStringName)
        {
        }

        public void Execute(Resource resource)
        {
            using (var cnn = CreateConnection())
            {
                try
                {
                    resource.ResourceId =
                        cnn.Query<int>(@"INSERT INTO Resource (ApplicationId, ResourceSet, ResourceKey, LocaleId, Value)
                                         VALUES (@ApplicationId, @ResourceSet, @ResourceKey, @LocaleId, @Value);
                                         SELECT SCOPE_IDENTITY();",
                            new
                            {
                                resource.ApplicationId,
                                resource.ResourceSet,
                                resource.ResourceKey,
                                resource.LocaleId,
                                resource.Value
                            }).First();
                }
                catch (DbException exception)
                {
                    throw new CreateResourceException(exception);
                }
            }
        }
    }
}