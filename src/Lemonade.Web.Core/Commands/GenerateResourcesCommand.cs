namespace Lemonade.Web.Core.Commands
{
    public class GenerateResourcesCommand : ICommand
    {
        public int ApplicationId { get; }
        public int LocaleId { get; }
        public int TargetLocaleId { get; }
        public string TranslationType { get; }

        public GenerateResourcesCommand(int applicationId, int localeId, int targetLocaleId, string translationType)
        {
            ApplicationId = applicationId;
            LocaleId = localeId;
            TargetLocaleId = targetLocaleId;
            TranslationType = translationType;
        }
    }
}