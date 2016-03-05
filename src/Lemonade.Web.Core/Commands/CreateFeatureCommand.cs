namespace Lemonade.Web.Core.Commands
{
    public class CreateFeatureCommand : ICommand
    {
        public bool IsEnabled { get; }
        public string Name { get; }
        public int ApplicationId { get; }

        public CreateFeatureCommand(string name, int applicationId, bool isEnabled)
        {
            IsEnabled = isEnabled;
            Name = name;
            ApplicationId = applicationId;
        }
    }
}