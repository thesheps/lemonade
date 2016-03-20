using FluentMigrator;

namespace Lemonade.Sql.Migrations
{
    [Migration(4)]
    public class CreateFeatureOverrideTable : Migration
    {
        public override void Up()
        {
            Create.Table("FeatureOverride")
                .WithColumn("FeatureOverrideId").AsInt32().PrimaryKey("PK_FeatureOverrideId").Identity()
                .WithColumn("FeatureId").AsInt32()
                .WithColumn("Hostname").AsString()
                .WithColumn("IsEnabled").AsBoolean();

            Create.Index("UK_Application_Feature")
                .OnTable("FeatureOverride")
                .OnColumn("FeatureId").Ascending()
                .OnColumn("Hostname").Ascending()
                .WithOptions()
                .Unique();

            Create.ForeignKey("FK_FeatureOverride_FeatureId")
               .FromTable("FeatureOverride").ForeignColumn("FeatureId")
               .ToTable("Feature").PrimaryColumn("FeatureId")
               .OnDeleteOrUpdate(System.Data.Rule.Cascade);
        }

        public override void Down()
        {
            Delete.Table("FeatureOverride");
        }
    }
}