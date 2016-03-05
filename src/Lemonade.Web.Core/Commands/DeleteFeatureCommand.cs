namespace Lemonade.Web.Core.Commands
{
    public class DeleteFeatureCommand : ICommand
    {
        public int FeatureId { get; }

        public DeleteFeatureCommand(int featureId)
        {
            FeatureId = featureId;
        }
    }
}