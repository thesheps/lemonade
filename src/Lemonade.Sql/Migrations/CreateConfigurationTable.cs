using FluentMigrator;

namespace Lemonade.Sql.Migrations
{
    [Migration(5)]
    public class CreateConfigurationTable : Migration
    {
        public override void Up()
        {
            Create.Table("Configuration")
                .WithColumn("ConfigurationId").AsInt32().PrimaryKey("PK_FeatureId").Identity()
                .WithColumn("ApplicationId").AsInt32()
                .WithColumn("Name").AsString()
                .WithColumn("Value").AsString();

            Create.Index("UK_Application_Configuration")
                .OnTable("Configuration")
                .OnColumn("ApplicationId").Ascending()
                .OnColumn("Name").Ascending()
                .WithOptions()
                .Unique();

            Create.ForeignKey("FK_Configuration_ApplicationId")
               .FromTable("Configuration").ForeignColumn("ApplicationId")
               .ToTable("Application").PrimaryColumn("ApplicationId")
               .OnDeleteOrUpdate(System.Data.Rule.Cascade);
        }

        public override void Down()
        {
            Delete.Table("Configuration");
        }
    }
}