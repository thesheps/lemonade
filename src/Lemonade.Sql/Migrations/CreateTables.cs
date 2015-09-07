using FluentMigrator;

namespace Lemonade.Sql.Migrations
{
    [Migration(1)]
    public class CreateTables : Migration
    {
        public override void Up()
        {
            Create.Table("Feature")
                .WithColumn("Id").AsInt32().PrimaryKey("PK_FeatureId").Identity()
                .WithColumn("ApplicationName").AsString()
                .WithColumn("FeatureName").AsString()
                .WithColumn("StartDate").AsDateTime()
                .WithColumn("ExpirationDays").AsInt32().Nullable()
                .WithColumn("IsEnabled").AsBoolean();

            Create.Index("UK_Application_Feature")
                .OnTable("Feature")
                .OnColumn("ApplicationName").Ascending()
                .OnColumn("FeatureName").Ascending();
        }

        public override void Down()
        {
            Delete.Table("Feature");
        }
    }
}