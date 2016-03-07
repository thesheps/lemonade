namespace Lemonade.Web.Core.Commands
{
    public class CreateResourceCommand : ICommand
    {
        public int ApplicationId { get; }
        public int LocaleId { get; }
        public string ResourceKey { get; }
        public string ResourceSet { get; }
        public string Value { get; }

        public CreateResourceCommand(int applicationId, int localeId, string resourceKey, string resourceSet, string value)
        {
            ApplicationId = applicationId;
            LocaleId = localeId;
            ResourceKey = resourceKey;
            ResourceSet = resourceSet;
            Value = value;
        }
    }
}