using Lemonade.Core;
using Lemonade.Resolvers;

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