using System.Data.Common;
using Dapper;
using Lemonade.Data.Commands;
using Lemonade.Data.Exceptions;

namespace Lemonade.Sql.Commands
{
    public class UpdateResource : LemonadeConnection, IUpdateResource
    {
        public UpdateResource()
        {
        }

        public UpdateResource(string connectionStringName) : base(connectionStringName)
        {
        }

        public void Execute(Data.Entities.Resource resource)
        {
            using (var cnn = CreateConnection())
            {
                try
                {
                    cnn.Execute(@"UPDATE Resource
                                  SET ResourceSet = @ResourceSet, 
                                  ResourceKey = @ResourceKey, 
                                  Locale = @Locale, 
                                  Value = @Value
                                  WHERE ResourceId = @ResourceId", new
                    {
                        resource.ResourceSet,
                        resource.ResourceKey,
                        resource.Locale,
                        resource.Value,
                        resource.ResourceId
                    });
                }
                catch (DbException exception)
                {
                    throw new UpdateResourceException(exception);
                }
            }
        }
    }
}