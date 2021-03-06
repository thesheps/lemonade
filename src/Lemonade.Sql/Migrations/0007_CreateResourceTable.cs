﻿using FluentMigrator;

namespace Lemonade.Sql.Migrations
{
    [Migration(7)]
    public class CreateResourceTable : Migration
    {
        public override void Up()
        {
            Create.Table("Resource")
                .WithColumn("ResourceId").AsInt32().PrimaryKey("PK_ResourceId").Identity()
                .WithColumn("ApplicationId").AsInt32()
                .WithColumn("LocaleId").AsInt32()
                .WithColumn("ResourceSet").AsString()
                .WithColumn("ResourceKey").AsString()
                .WithColumn("Value").AsCustom("NVARCHAR(4000)");

            Create.Index("UK_Application_Resource_Locale")
                .OnTable("Resource")
                .OnColumn("ApplicationId").Ascending()
                .OnColumn("LocaleId").Ascending()
                .OnColumn("ResourceSet").Ascending()
                .OnColumn("ResourceKey").Ascending()
                .WithOptions()
                .Unique();

            Create.ForeignKey("FK_Resource_ApplicationId")
               .FromTable("Resource").ForeignColumn("ApplicationId")
               .ToTable("Application").PrimaryColumn("ApplicationId")
               .OnDeleteOrUpdate(System.Data.Rule.Cascade);

            Create.ForeignKey("FK_Resource_LocaleId")
               .FromTable("Resource").ForeignColumn("LocaleId")
               .ToTable("Locale").PrimaryColumn("LocaleId")
               .OnDeleteOrUpdate(System.Data.Rule.Cascade);
        }

        public override void Down()
        {
            Delete.Table("Resource");
        }
    }
}