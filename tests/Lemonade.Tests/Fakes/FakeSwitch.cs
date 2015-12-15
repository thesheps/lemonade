namespace Lemonade.Resolvers.Fakes
{
    public class FakeSwitch : FeatureWrapper
    {
        protected override string Key => "UseTestFunctionality";

        public bool Executed { get; set; }

        protected override void OnExecute()
        {
            Executed = true;
        }
    }
}