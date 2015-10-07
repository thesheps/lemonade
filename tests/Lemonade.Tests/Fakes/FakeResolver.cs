using Lemonade.Resolvers;

namespace Lemonade.Tests.Fakes
{
    public class FakeResolver : IFeatureResolver
    {
        public bool Resolve(string featureName, string applicationName)
        {
            return featureName == "UseTestFunctionality";
        }
    }
}