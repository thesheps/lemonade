using System.Data.Common;
using System.Linq;
using Dapper;
using Lemonade.Core.Commands;
using Lemonade.Core.Domain;
using Lemonade.Core.DomainEvents;
using Lemonade.Core.Exceptions;

namespace Lemonade.Sql.Commands
{
    public class SaveApplication : LemonadeConnection, ISaveApplication
    {
        public SaveApplication()
        {
        }

        public SaveApplication(string connectionStringName) : base(connectionStringName)
        {
        }

        public void Execute(Application application)
        {
            using (var cnn = CreateConnection())
            {
                try
                {
                    cnn.Execute("INSERT INTO Application (Name) VALUES (@Name)", new { application.Name });
                    application.ApplicationId = cnn.Query<int>("SELECT CAST(@@IDENTITY AS INT)").First();

                    DomainEvent.Raise(new ApplicationHasBeenSaved(application.ApplicationId));
                }
                catch (DbException exception)
                {
                    throw new SaveApplicationException(application.Name, exception);
                }
            }
        }
    }
}