using Lemonade.Exceptions;
using Lemonade.Tests.Fakes;
using NUnit.Framework;

namespace Lemonade.Tests
{
    public class GivenFeatureSwitches
    {
        [Test]
        public void WhenUsingFeatureIndexAndMethodIsFeatureSwitchedOn_ThenItIsExecuted()
        {
            Feature.SetResolver(new FakeResolver());
            var executed = Feature.Switches["UseTestFunctionality"];
            Assert.That(executed, Is.True);
        }

        [Test]
        public void WhenUsingDynamicIndexAndMethodIsFeatureSwitchedOn_ThenItIsExecuted()
        {
            Feature.SetResolver(new FakeResolver());
            var executed = Feature.Switches[d => d.UseTestFunctionality];
            Assert.That(executed, Is.True);
        }

        [Test]
        public void WhenUsingFeatureWrapperAndMethodIsFeatureSwitchedOn_ThenItIsExecuted()
        {
            var executed = false;
            Feature.SetResolver(new FakeResolver());
            Feature.Switches.Execute("UseTestFunctionality", () => executed = true);
            Assert.That(executed, Is.True);
        }

        [Test]
        public void WhenUsingDynamicFeatureWrapperAndMethodIsFeatureSwitchedOn_ThenItIsExecuted()
        {
            var executed = false;
            Feature.SetResolver(new FakeResolver());
            Feature.Switches.Execute(d => d.UseTestFunctionality, () => executed = true);
            Assert.That(executed, Is.True);
        }

        [Test]
        public void WhenNoResolverHasBeenSet_ThenResolverNotFoundExceptionIsThrown()
        {
            Feature.SetResolver(null);
            Assert.Throws<ResolverNotFoundException>(() => { var executed = Feature.Switches["UseTestFunctionality"]; });
        }

        [Test]
        public void WhenUnknownFeature_ThenUnknownFeatureExceptionIsThrown()
        {
            Feature.SetResolver(new FakeResolver());
            Assert.Throws<UnknownFeatureException>(() => { var executed = Feature.Switches["Nonsense"]; });
        }
    }
}