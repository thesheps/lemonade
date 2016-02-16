using Lemonade.Resolvers.Fakes;
using Lemonade.Tests.Fakes;
using NUnit.Framework;

namespace Lemonade.Tests
{
    public class GivenFeatureSwitch
    {
        [Test]
        public void WhenFeatureSwitchedOn_ThenFeatureIsExecuted()
        {
            Configuration.FeatureResolver = new FakeResolver();
            var featureSwitch = new FakeSwitch();
            featureSwitch.Execute();

            Assert.That(featureSwitch.IsEnabled, Is.True);
            Assert.That(featureSwitch.Executed, Is.True);
        } 
    }
}