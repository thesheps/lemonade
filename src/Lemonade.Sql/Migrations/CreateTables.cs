using FluentMigrator;

namespace Lemonade.Sql.Migrations
{
    [Migration(1)]
    public class CreateTables : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("Feature")
                .WithColumn("Id").AsInt32().PrimaryKey("PK_FeatureId").Identity()
                .WithColumn("Application").AsString()
                .WithColumn("Name").AsString()
                .WithColumn("StartDate").AsDateTime()
                .WithColumn("ExpirationDays").AsInt32().Nullable()
                .WithColumn("IsEnabled").AsBoolean();
        }
    }
}