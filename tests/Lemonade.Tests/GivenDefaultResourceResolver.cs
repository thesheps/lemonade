using System.Globalization;
using System.Web;
using System.Web.Compilation;
using Lemonade.Core.Services;
using NSubstitute;
using NUnit.Framework;

namespace Lemonade.Tests
{
    public class GivenDefaultResourceResolver
    {
        [TestCase("en-GB", "Hello World")]
        [TestCase("de-DE", "Tag Weld")]
        [TestCase("fr-FR", "Bonjour le monde")]
        public void WhenIObtainAGlobalResourceString_ThenItIsObtainedForTheCorrectLocale(string locale, string expectedOutput)
        {
            var culture = CultureInfo.CreateSpecificCulture(locale);
            var test = HttpContext.GetGlobalResourceObject("Hello", "World", culture);

            Assert.That(test, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void WhenResourceProviderIsSpecified_ThenItIsUsedToProcureResources()
        {
            var resourceProvider = Substitute.For<IResourceProvider>();
            var resourceResolver = Substitute.For<IResourceResolver>();
            resourceProvider.GetObject(Arg.Any<string>(), Arg.Any<CultureInfo>()).Returns("Test");
            resourceResolver.Create(Arg.Any<string>()).Returns(resourceProvider);

            Configuration.ResourceResolver = resourceResolver;

            var test = HttpContext.GetGlobalResourceObject("Ponies", "Sheep");
            Assert.That(test, Is.EqualTo("Test"));
        }
    }
}