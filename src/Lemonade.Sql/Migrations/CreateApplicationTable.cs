using FluentMigrator;

namespace Lemonade.Sql.Migrations
{
    [Migration(2)]
    public class CreateApplicationTable : Migration
    {
        public override void Up()
        {
            Create.Table("Application")
                .WithColumn("Id").AsInt32().PrimaryKey("PK_ApplicationId").Identity()
                .WithColumn("Name").AsString();

            Create.Index("UK_Application")
                .OnTable("Application")
                .OnColumn("Name").Ascending()
                .WithOptions()
                .Unique();
        }

        public override void Down()
        {
            Delete.Table("Application");
        }
    }
}