﻿using System;
using Lemonade.Builders;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Migrations;
using Lemonade.Sql.Queries;
using NUnit.Framework;

namespace Lemonade.Sql.Tests
{
    public class GivenDeleteApplication
    {
        [SetUp]
        public void SetUp()
        {
            _createFeature = new CreateFeature();
            _createApplication = new CreateApplication();
            _deleteApplication = new DeleteApplication();
            _getApplicationByName = new GetApplicationByName();
            Runner.SqlCompact("Lemonade").Down();
            Runner.SqlCompact("Lemonade").Up();
        }

        [Test]
        public void WhenIDeleteAnApplication_ThenItIsNoLongerAvailable()
        {
            var application = new ApplicationBuilder()
                .WithName("Test12345")
                .Build();

            _createApplication.Execute(application);
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

            _createApplication.Execute(application);
            application = _getApplicationByName.Execute(application.Name);

            var feature = new FeatureBuilder()
                .WithName("SuperFeature123")
                .WithApplication(application)
                .WithStartDate(DateTime.Now)
                .Build();

            _createFeature.Execute(feature);
            _deleteApplication.Execute(application.ApplicationId);
            application = _getApplicationByName.Execute(application.Name);

            Assert.That(application, Is.Null);
        }

        private GetApplicationByName _getApplicationByName;
        private CreateApplication _createApplication;
        private DeleteApplication _deleteApplication;
        private CreateFeature _createFeature;
    }
}