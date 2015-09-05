using Lemonade.Exceptions;
using NUnit.Framework;

namespace Lemonade.Web.Tests
{
    public class GivenFeatureSwitches
    {
        [Test]
        public void WhenNoResolverHasBeenSet_ThenResolverNotFoundExceptionIsThrown()
        {
            Feature.Resolver(null);
            Assert.Throws<ResolverNotFoundException>(() => { var executed = Feature.Switches["UseTestFunctionality"]; });
        }
    }
}