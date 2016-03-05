namespace Lemonade.Web.Core.Commands
{
    public class CreateResourceCommand : ICommand
    {
        public int ApplicationId { get; }
        public string Locale { get; }
        public string ResourceKey { get; }
        public string ResourceSet { get; }
        public string Value { get; }

        public CreateResourceCommand(int applicationId, string locale, string resourceKey, string resourceSet, string value)
        {
            ApplicationId = applicationId;
            Locale = locale;
            ResourceKey = resourceKey;
            ResourceSet = resourceSet;
            Value = value;
        }
    }
}