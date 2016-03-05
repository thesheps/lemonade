namespace Lemonade.Web.Core.Commands
{
    public class DeleteConfigurationCommand : ICommand
    {
        public int ConfigurationId { get; }

        public DeleteConfigurationCommand(int configurationId)
        {
            ConfigurationId = configurationId;
        }
    }
}