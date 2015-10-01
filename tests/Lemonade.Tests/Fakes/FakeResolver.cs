using Lemonade.Core;

namespace Lemonade.Tests.Fakes
{
    public class FakeResolver : IFeatureResolver
    {
        public bool Resolve(string featureName)
        {
            return featureName == "UseTestFunctionality";
        }
    }
}