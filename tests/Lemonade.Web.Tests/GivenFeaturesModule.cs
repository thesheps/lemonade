using Lemonade.Data.Queries;
using Lemonade.Web.Modules;
using Nancy;
using Nancy.Testing;
using NSubstitute;
using NUnit.Framework;

namespace Lemonade.Web.Tests
{
    public class GivenFeaturesModule
    {
        [SetUp]
        public void SetUp()
        {
            _browser = new Browser(new ConfigurableBootstrapper(with =>
            {
                with.Module<FeaturesModule>();
                with.Dependency(Substitute.For<IGetAllFeatures>());
            }));
        }

        [Test]
        public void WhenIGetFeatures_ThenViewIsFoundAndResponseIsOK()
        {
            Assert.That(_browser.Get("/features").StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        private Browser _browser;
    }
}