using System.Data.Common;
using System.Linq;
using Dapper;
using Lemonade.Data.Commands;
using Lemonade.Data.Entities;
using Lemonade.Data.Exceptions;
using Lemonade.Sql;

namespace Lemonade.Fakes
{
    public class CreateResourceFake : LemonadeConnection, ICreateResource
    {
        public CreateResourceFake()
        {
        }

        public CreateResourceFake(string connectionStringName) : base(connectionStringName)
        {
        }

        public void Execute(Resource resource)
        {
            using (var cnn = CreateConnection())
            {
                try
                {
                    cnn.Execute(@"INSERT INTO Resource (ApplicationId, ResourceSet, ResourceKey, Locale, Value)
                                  VALUES (@ApplicationId, @ResourceSet, @ResourceKey, @Locale, @Value)", 
                    new
                    {
                        resource.ApplicationId,
                        resource.ResourceSet,
                        resource.ResourceKey,
                        resource.Locale,
                        resource.Value
                    });

                    resource.ResourceId = cnn.Query<int>("SELECT CAST(@@IDENTITY AS INT)").First();
                }
                catch (DbException exception)
                {
                    throw new CreateResourceException(exception);
                }
            }
        }
    }
}