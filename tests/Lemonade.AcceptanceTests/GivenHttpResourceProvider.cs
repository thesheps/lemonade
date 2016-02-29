using System.Globalization;
using System.Web;
using System.Web.UI;
using NUnit.Framework;

namespace Lemonade.AcceptanceTests
{
    public class GivenHttpResourceProvider : TemplateControl
    {
        [TestCase("en-GB", "Hello World")]
        [TestCase("de-DE", "Tag Weld")]
        [TestCase("fr-FR", "Bonjour le monde")]
        public void WhenIObtainAGlobalResourceString_ThenItObtainedForTheCorrectLocale(string locale, string expectedOutput)
        {
            var culture = CultureInfo.CreateSpecificCulture(locale);
            var test = HttpContext.GetGlobalResourceObject("Hello", "World", culture);

            Assert.That(test, Is.EqualTo(expectedOutput));
        }
    }
}