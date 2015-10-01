using Lemonade.Core;

namespace Lemonade.Tests.Fakes
{
    public class FakeResolver : IFeatureResolver
    {
        public bool Get(string featureName)
        {
            return featureName == "UseTestFunctionality";
        }
    }
}