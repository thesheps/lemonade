using System;
using Lemonade.Builders;
using Lemonade.Core.Events;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Migrations;
using Lemonade.Sql.Queries;
using NUnit.Framework;

namespace Lemonade.Sql.Tests
{
    public class GivenDeleteFeature : IDomainEventDispatcher
    {
        [SetUp]
        public void SetUp()
        {
            DomainEvent.Dispatcher = this;
            _saveFeature = new SaveFeature();
            _saveApplication = new SaveApplication();
            _deleteFeature = new DeleteFeature();
            _getApplicationByName = new GetApplicationByName();
            _getFeatureByNameAndApplication = new GetFeatureByNameAndApplication();
            Runner.SqlCompact("Lemonade").Down();
            Runner.SqlCompact("Lemonade").Up();
        }

        [Test]
        public void WhenIDeleteAFeature_ThenFeatureHasBeenDeletedEventIsRaisedWithCorrectInformation()
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
            feature = _getFeatureByNameAndApplication.Execute(feature.Name, application.Name);

            _deleteFeature.Execute(feature.FeatureId);

            Assert.That(_deletedFeature.FeatureId, Is.EqualTo(feature.FeatureId));
        }

        [Test]
        public void WhenIDeleteAFeature_ThenItIsNoLongerAvailable()
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
            feature = _getFeatureByNameAndApplication.Execute(feature.Name, application.Name);

            _deleteFeature.Execute(feature.FeatureId);
            feature = _getFeatureByNameAndApplication.Execute(feature.Name, application.Name);

            Assert.That(feature, Is.Null);
        }

        public void Dispatch<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            if (@event is FeatureHasBeenDeleted) _deletedFeature = @event as FeatureHasBeenDeleted;
        }

        private FeatureHasBeenDeleted _deletedFeature;
        private GetFeatureByNameAndApplication _getFeatureByNameAndApplication;
        private SaveApplication _saveApplication;
        private SaveFeature _saveFeature;
        private DeleteFeature _deleteFeature;
        private GetApplicationByName _getApplicationByName;
    }
}