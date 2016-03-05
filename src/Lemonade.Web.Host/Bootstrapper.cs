using Lemonade.Sql.Migrations;
using Lemonade.Web.Infrastructure;

namespace Lemonade.Web.Host
{
    public class Bootstrapper : LemonadeBootstrapper
    {
        public Bootstrapper()
        {
            Runner.SqlServer("Lemonade").Up();
        }
    }
}