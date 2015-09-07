using System;
using System.Data.SQLite.EF6;
using Dapper;
using Lemonade.Sql.Exceptions;
using NUnit.Framework;

namespace Lemonade.Sql.Tests
{
    public class GivenSqlFeatureResolver
    {
        const string ConnectionString = "FullUri=file::memory:?cache=shared";

        [SetUp]
        public void Setup()
        {
            DbMigrations.Sqlite(ConnectionString).Up();
            InsertFeature(true, "MyEnabledFeature");
            InsertFeature(false, "MyDisabledFeature");
        }

        [Test]
        public void WhenIObtainAnSqlFeatureResolverViaConfiguration_AndDefaultConnectionStringExists_ThenResolverIsSet()
        {
            Assert.That(Feature.Resolver, Is.AssignableFrom<SqlFeatureResolver>());
        }

        [Test]
        public void WhenITryToObtainAnSqlFeatureResolverForANonExistantConnectionString_ThenConnectionStringNotFoundExceptionIsThrown()
        {
            Assert.Throws<ConnectionStringNotFoundException>(() =>
            {
                Feature.Resolver = new SqlFeatureResolver("ThisIsATest");
            });
        }

        [Test]
        public void WhenIGetAValidEnabledFeature_ThenReturnedValueIsTrue()
        {
            var executed = false;
            Feature.Switches.Execute("MyEnabledFeature", () => executed = true);
            Assert.That(executed, Is.True);
        }

        [Test]
        public void WhenIGetAValidDisabledFeature_ThenReturnedValueIsFalse()
        {
            var executed = false;
            Feature.Switches.Execute("MyDisabledFeature", () => executed = true);
            Assert.That(executed, Is.False);
        }

        private static void InsertFeature(bool isEnabled, string name)
        {
            using (var cnn = new SQLiteProviderFactory().CreateConnection())
            {
                if (cnn != null) cnn.ConnectionString = ConnectionString;

                cnn.Execute("INSERT INTO Feature (IsEnabled, ExpirationDays, StartDate, Name, Application)" +
                            "VALUES(@isEnabled, @expirationDays, @startDate, @name, @application)", new
                            {
                                isEnabled,
                                expirationDays = 1,
                                startDate = DateTime.Now,
                                name,
                                application = "TestApplication"
                            });
            }
        }
    }
}