namespace Lemonade.Web.Core.Commands
{
    public class GenerateResourcesCommand : ICommand
    {
        public int ApplicationId { get; }
        public int LocaleId { get; }
        public int TargetLocaleId { get; }
        public string Type { get; }

        public GenerateResourcesCommand(int applicationId, int localeId, int targetLocaleId, string type)
        {
            ApplicationId = applicationId;
            LocaleId = localeId;
            TargetLocaleId = targetLocaleId;
            Type = type;
        }
    }
}