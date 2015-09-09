using Lemonade.Resolvers;

namespace Lemonade.Tests.Fakes
{
    public class FakeResolver : IFeatureResolver
    {
        public bool Get(string value)
        {
            return value == "UseTestFunctionality";
        }
    }
}