namespace Lemonade.Web.Core.Commands
{
    public class CreateConfigurationCommand : ICommand
    {
        public int ApplicationId { get; }
        public string Name { get; }
        public string Value { get; }

        public CreateConfigurationCommand(int applicationId, string name, string value)
        {
            ApplicationId = applicationId;
            Name = name;
            Value = value;
        }
    }
}