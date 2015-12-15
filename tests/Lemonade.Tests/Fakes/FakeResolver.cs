namespace Lemonade.Resolvers.Fakes
{
    public class FakeResolver : IFeatureResolver
    {
        public bool Resolve(string featureName, string applicationName)
        {
            return featureName == "UseTestFunctionality";
        }
    }
}