using System;
using Lemonade.Builders;
using Lemonade.Core.Events;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Migrations;
using Lemonade.Sql.Queries;
using NUnit.Framework;

namespace Lemonade.Sql.Tests
{
    public class GivenDeleteApplication : IDomainEventDispatcher
    {
        [SetUp]
        public void SetUp()
        {
            DomainEvent.Dispatcher = this;
            _saveFeature = new SaveFeature();
            _saveApplication = new SaveApplication();
            _deleteApplication = new DeleteApplication();
            _getApplicationByName = new GetApplicationByName();
            Runner.SqlCompact("Lemonade").Down();
            Runner.SqlCompact("Lemonade").Up();
        }

        [Test]
        public void WhenIDeleteAnApplication_ThenApplicationHasBeenDeletedEventIsRaisedWithCorrectInformation()
        {
            var application = new ApplicationBuilder()
                .WithName("Test12345")
                .Build();

            _saveApplication.Execute(application);
            application = _getApplicationByName.Execute(application.Name);
            _deleteApplication.Execute(application.ApplicationId);

            Assert.That(_deletedApplication.ApplicationId, Is.EqualTo(application.ApplicationId));
        }

        [Test]
        public void WhenIDeleteAnApplication_ThenItIsNoLongerAvailable()
        {
            var application = new ApplicationBuilder()
                .WithName("Test12345")
                .Build();

            _saveApplication.Execute(application);
            application = _getApplicationByName.Execute(application.Name);
            _deleteApplication.Execute(application.ApplicationId);
            application = _getApplicationByName.Execute(application.Name);

            Assert.That(application, Is.Null);
        }

        [Test]
        public void WhenIDeleteAnApplicationWithAssociatedFeatures_ThenItIsNoLongerAvailable()
        {
            var application = new ApplicationBuilder()
                .WithName("Test12345")
                .Build();

            _saveApplication.Execute(application);
            application = _getApplicationByName.Execute(application.Name);

            var feature = new FeatureBuilder()
                .WithName("SuperFeature123")
                .WithApplication(application)
                .WithStartDate(DateTime.Now)
                .Build();

            _saveFeature.Execute(feature);
            _deleteApplication.Execute(application.ApplicationId);
            application = _getApplicationByName.Execute(application.Name);

            Assert.That(application, Is.Null);
        }

        public void Dispatch<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            if (@event is ApplicationHasBeenDeleted) _deletedApplication = @event as ApplicationHasBeenDeleted;
        }

        private GetApplicationByName _getApplicationByName;
        private SaveApplication _saveApplication;
        private DeleteApplication _deleteApplication;
        private SaveFeature _saveFeature;
        private ApplicationHasBeenDeleted _deletedApplication;
    }
}