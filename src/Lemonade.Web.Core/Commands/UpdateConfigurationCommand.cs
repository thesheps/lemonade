namespace Lemonade.Web.Core.Commands
{
    public class UpdateConfigurationCommand : ICommand
    {
        public int ConfigurationId { get; }
        public string Value { get; }
        public string Name { get; }

        public UpdateConfigurationCommand(int configurationId, string name, string value)
        {
            ConfigurationId = configurationId;
            Name = name;
            Value = value;
        }
    }
}