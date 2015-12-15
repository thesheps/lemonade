using Lemonade.Resolvers.Fakes;
using NUnit.Framework;

namespace Lemonade.Resolvers
{
    public class GivenFeatureSwitch
    {
        [Test]
        public void WhenFeatureSwitchedOn_ThenFeatureIsExecuted()
        {
            Lemonade.FeatureResolver = new FakeResolver();
            var featureSwitch = new FakeSwitch();
            featureSwitch.Execute();

            Assert.That(featureSwitch.IsEnabled, Is.True);
            Assert.That(featureSwitch.Executed, Is.True);
        } 
    }
}