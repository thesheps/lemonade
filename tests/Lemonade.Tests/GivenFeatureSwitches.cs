using Lemonade.Exceptions;
using Lemonade.Tests.Fakes;
using NUnit.Framework;

namespace Lemonade.Tests
{
    public class GivenFeatureSwitches
    {
        [Test]
        public void WhenUsingFeatureIndexAndMethodIsFeatureSwitchedOff_ThenItIsNotExecuted()
        {
            Feature.SetResolver(new FakeResolver());
            var executed = Feature.Switches["UseTestFunctionality"];
            Assert.That(executed, Is.False);
        }

        [Test]
        public void WhenNoResolverHasBeenSet_ThenResolverNotFoundExceptionIsThrown()
        {
            Assert.Throws<ResolverNotFoundException>(() =>
            {
                var executed = Feature.Switches["UseTestFunctionality"];
            });
        }

        [Test]
        public void WhenUnknownFeature_ThenUnknownFeatureExceptionIsThrown()
        {
            Feature.SetResolver(new FakeResolver());
            Assert.Throws<UnknownFeatureException>(() =>
            {
                var executed = Feature.Switches["Nonsense"];
            });
        }
    }
}