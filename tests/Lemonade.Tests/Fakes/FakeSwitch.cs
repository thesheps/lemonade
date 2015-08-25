namespace Lemonade.Tests.Fakes
{
    public class FakeSwitch : FeatureSwitch
    {
        protected override string Key => "UseTestFunctionality";

        public bool Executed { get; set; }

        protected override void OnExecute()
        {
            Executed = true;
        }
    }
}