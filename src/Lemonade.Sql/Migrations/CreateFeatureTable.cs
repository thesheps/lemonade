using FluentMigrator;

namespace Lemonade.Sql.Migrations
{
    [Migration(2)]
    public class CreateFeatureTable : Migration
    {
        public override void Up()
        {
            Create.Table("Feature")
                .WithColumn("FeatureId").AsInt32().PrimaryKey("PK_FeatureId").Identity()
                .WithColumn("ApplicationId").AsInt32()
                .WithColumn("Name").AsString()
                .WithColumn("StartDate").AsDateTime()
                .WithColumn("ExpirationDays").AsInt32().Nullable()
                .WithColumn("IsEnabled").AsBoolean();

            Create.Index("UK_Application_Feature")
                .OnTable("Feature")
                .OnColumn("ApplicationId").Ascending()
                .OnColumn("Name").Ascending()
                .WithOptions()
                .Unique();

            Create.ForeignKey("FK_Feature_ApplicationId")
               .FromTable("Feature").ForeignColumn("ApplicationId")
               .ToTable("Application").PrimaryColumn("ApplicationId")
               .OnDeleteOrUpdate(System.Data.Rule.Cascade);
        }

        public override void Down()
        {
            Delete.Table("Feature");
        }
    }
}