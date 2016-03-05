namespace Lemonade.Web.Core.Commands
{
    public class DeleteFeatureOverrideCommand : ICommand
    {
        public int FeatureOverrideId { get; }

        public DeleteFeatureOverrideCommand(int featureOverrideId)
        {
            FeatureOverrideId = featureOverrideId;
        }
    }
}