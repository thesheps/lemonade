using System;
using Lemonade.Builders;
using Lemonade.Core.Events;
using Lemonade.Core.Exceptions;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Migrations;
using Lemonade.Sql.Queries;
using NUnit.Framework;

namespace Lemonade.Sql.Tests
{
    public class GivenSaveFeature : IDomainEventDispatcher
    {
        [SetUp]
        public void SetUp()
        {
            DomainEvent.Dispatcher = this;
            Runner.SqlCompact("Lemonade").Down();
            Runner.SqlCompact("Lemonade").Up();
        }

        [Test]
        public void WhenITryToSaveAFeature_ThenFeatureHasBeenSavedEventIsRaisedWithCorrectDetails()
        {
            var saveFeature = new SaveFeature();
            var saveApplication = new SaveApplication();
            var getApplicationByName = new GetApplicationByName();
            var getFeatureByNameAndApplication = new GetFeatureByNameAndApplication();

            var application = new ApplicationBuilder()
                .WithName("Test12345")
                .Build();

            saveApplication.Execute(application);
            application = getApplicationByName.Execute(application.Name);

            var feature = new FeatureBuilder()
                .WithName("MyTestFeature")
                .WithStartDate(DateTime.Now)
                .WithApplication(application).Build();

            saveFeature.Execute(feature);

            feature = getFeatureByNameAndApplication.Execute(feature.Name, application.Name);

            Assert.That(_savedFeature.FeatureId, Is.EqualTo(feature.FeatureId));
        }

        [Test]
        public void WhenITryToSaveADuplicateFeature_ThenSaveFeatureExceptionIsThrown()
        {
            var saveFeature = new SaveFeature();
            var saveApplication = new SaveApplication();
            var getApplicationByName = new GetApplicationByName();

            var application = new ApplicationBuilder()
                .WithName("Test12345")
                .Build();

            saveApplication.Execute(application);
            application = getApplicationByName.Execute(application.Name);

            var feature = new FeatureBuilder()
                .WithName("MyTestFeature")
                .WithStartDate(DateTime.Now)
                .WithApplication(application).Build();

            saveFeature.Execute(feature);

            Assert.Throws<SaveFeatureException>(() => saveFeature.Execute(feature));
        }

        public void Dispatch<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            if (@event is FeatureHasBeenSaved) _savedFeature = @event as FeatureHasBeenSaved;
        }

        private FeatureHasBeenSaved _savedFeature;
    }
}