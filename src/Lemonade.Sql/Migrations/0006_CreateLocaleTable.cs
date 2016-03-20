using System.Globalization;
using FluentMigrator;

namespace Lemonade.Sql.Migrations
{
    [Migration(6)]
    public class CreateLocaleTable : Migration
    {
        public override void Up()
        {
            Create.Table("Locale")
                .WithColumn("LocaleId").AsInt32().PrimaryKey("PK_LocaleId").Identity()
                .WithColumn("IsoCode").AsString()
                .WithColumn("Description").AsString();

            Create.Index("UK_Locale")
                .OnTable("Locale")
                .OnColumn("IsoCode").Ascending()
                .OnColumn("Description").Ascending()
                .WithOptions()
                .Unique();

            foreach (var cultureInfo in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                Insert.IntoTable("Locale").Row(
                new
                {
                    Description = cultureInfo.EnglishName,
                    IsoCode = cultureInfo.Name
                });
            }
        }

        public override void Down()
        {
            Delete.Table("Locale");
        }
    }
}