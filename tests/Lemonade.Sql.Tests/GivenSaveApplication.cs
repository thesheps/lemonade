using Lemonade.Builders;
using Lemonade.Core.DomainEvents;
using Lemonade.Core.Exceptions;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Migrations;
using Lemonade.Sql.Queries;
using NSubstitute;
using NUnit.Framework;

namespace Lemonade.Sql.Tests
{
    public class GivenSaveApplication : IDomainEventDispatcher
    {
        [SetUp]
        public void SetUp()
        {
            DomainEvent.Dispatcher = this;
            Runner.SqlCompact("Lemonade").Down();
            Runner.SqlCompact("Lemonade").Up();
            _applicationHasBeenSavedHandler = Substitute.For<IDomainEventHandler<ApplicationHasBeenSaved>>();
            _applicationHasBeenSavedHandler
                .When(a => a.Handle(Arg.Any<ApplicationHasBeenSaved>()))
                .Do(a => _savedApplication = a.Arg<ApplicationHasBeenSaved>());
        }

        [Test]
        public void WhenISaveAnApplication_ThenICanRetrieveIt()
        {
            var saveApplication = new SaveApplication();
            var getApplicationByName = new GetApplicationByName();
            saveApplication.Execute(new ApplicationBuilder()
                .WithName("Test12345")
                .Build());

            var application = getApplicationByName.Execute("Test12345");
            Assert.That(application, Is.Not.Null);
            Assert.That(application.Name, Is.EqualTo("Test12345"));
        }

        [Test]
        public void WhenISaveAnApplication_ThenApplicationSavedEventIsRaisedWithCorrectApplicationId()
        {
            var saveApplication = new SaveApplication();
            var getApplicationByName = new GetApplicationByName();
            saveApplication.Execute(new ApplicationBuilder()
                .WithName("Test12345")
                .Build());

            var application = getApplicationByName.Execute("Test12345");

            Assert.That(_savedApplication.ApplicationId, Is.EqualTo(application.ApplicationId));
        }

        [Test]
        public void WhenITryToSaveADuplicateApplication_ThenSaveApplicationExceptionIsThrown()
        {
            var saveApplication = new SaveApplication();
            var application = new ApplicationBuilder()
                .WithName("Test12345")
                .Build();

            saveApplication.Execute(application);

            Assert.Throws<SaveApplicationException>(() => saveApplication.Execute(application));
        }

        public void Dispatch<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            if (@event is ApplicationHasBeenSaved) _applicationHasBeenSavedHandler.Handle(@event as ApplicationHasBeenSaved);
        }

        private IDomainEventHandler<ApplicationHasBeenSaved> _applicationHasBeenSavedHandler;
        private ApplicationHasBeenSaved _savedApplication;
    }
}