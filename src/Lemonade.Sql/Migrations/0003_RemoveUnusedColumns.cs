using FluentMigrator;

namespace Lemonade.Sql.Migrations
{
    [Migration(3)]
    public class RemoveUnusedColumns : Migration
    {
        public override void Up()
        {
            Delete.Column("StartDate").FromTable("Feature");
            Delete.Column("ExpirationDays").FromTable("Feature");
        }

        public override void Down()
        {
        }
    }
}