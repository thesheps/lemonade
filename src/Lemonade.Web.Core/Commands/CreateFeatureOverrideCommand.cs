namespace Lemonade.Web.Core.Commands
{
    public class CreateFeatureOverrideCommand : ICommand
    {
        public int FeatureId { get; }
        public int FeatureOverrideId { get; }
        public string Hostname { get; }
        public bool IsEnabled { get; }

        public CreateFeatureOverrideCommand(int featureId, int featureOverrideId, string hostname, bool isEnabled)
        {
            FeatureId = featureId;
            FeatureOverrideId = featureOverrideId;
            Hostname = hostname;
            IsEnabled = isEnabled;
        }
    }
}