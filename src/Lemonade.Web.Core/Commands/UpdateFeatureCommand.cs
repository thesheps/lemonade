namespace Lemonade.Web.Core.Commands
{
    public class UpdateFeatureCommand : ICommand
    {
        public int FeatureId { get; }
        public string Name { get; }
        public bool IsEnabled { get; }

        public UpdateFeatureCommand(int featureId, string name, bool isEnabled)
        {
            FeatureId = featureId;
            Name = name;
            IsEnabled = isEnabled;
        }
    }
}