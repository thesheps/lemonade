namespace Lemonade.SqlServer.Migrations
{
    public class CreateTable : FluentMigrator.ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("Feature")
                .WithColumn("FeatureId").AsInt32().PrimaryKey("PK_FeatureId")
                .WithColumn("Name").AsString()
                .WithColumn("StartDate").AsDateTime()
                .WithColumn("ExpirationDays").AsInt32().Nullable()
                .WithColumn("IsEnabled").AsBoolean();
        }
    }
}