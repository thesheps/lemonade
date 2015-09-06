using System.Diagnostics;
using System.Reflection;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using FluentMigrator.Runner.Processors.SqlServer;
using FluentMigrator.Runner.Processors.SQLite;

namespace Lemonade.Sql
{
    public class DbMigrations
    {
        private DbMigrations(MigrationRunner runner)
        {
            _runner = runner;
        }

        public static DbMigrations SqlServer(string connectionString)
        {
            return new DbMigrations(GetRunner(connectionString, new SqlServer2008ProcessorFactory()));
        }

        public static DbMigrations Sqlite(string connectionString)
        {
            return new DbMigrations(GetRunner(connectionString, new SQLiteProcessorFactory()));
        }

        public void Up()
        {
            _runner.MigrateUp();
        }

        private static MigrationRunner GetRunner(string connectionString, MigrationProcessorFactory factory)
        {
            var announcer = new TextWriterAnnouncer(s => Debug.WriteLine(s));
            var assembly = Assembly.GetExecutingAssembly();
            var migrationContext = new RunnerContext(announcer) { Namespace = "Lemonade.Sql.Migrations" };
            var options = new MigrationOptions { PreviewOnly = false, Timeout = 60 };
            var processor = factory.Create(connectionString, announcer, options);

            return new MigrationRunner(assembly, migrationContext, processor);
        }

        private class MigrationOptions : IMigrationProcessorOptions
        {
            public bool PreviewOnly { get; set; }
            public int Timeout { get; set; }
            public string ProviderSwitches => string.Empty;
        }

        private readonly MigrationRunner _runner;
    }
}