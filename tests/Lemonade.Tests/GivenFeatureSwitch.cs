using Lemonade.Tests.Fakes;
using NUnit.Framework;

namespace Lemonade.Tests
{
    public class GivenFeatureSwitch
    {
        [Test]
        public void When()
        {
            Feature.SetResolver(new FakeResolver());
            var featureSwitch = new FakeSwitch();
            featureSwitch.Execute();

            Assert.That(featureSwitch.IsEnabled, Is.True);
            Assert.That(featureSwitch.Executed, Is.True);
        } 
    }
}