namespace Lemonade.Web.Core.Commands
{
    public class UpdateFeatureOverrideCommand : ICommand
    {
        public int FeatureId { get; }
        public int FeatureOverrideId { get; }
        public string Hostname { get; }
        public bool IsEnabled { get; }

        public UpdateFeatureOverrideCommand(int featureId, int featureOverrideId, string hostname, bool isEnabled)
        {
            FeatureId = featureId;
            FeatureOverrideId = featureOverrideId;
            Hostname = hostname;
            IsEnabled = isEnabled;
        }
    }
}