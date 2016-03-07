namespace Lemonade.Web.Core.Commands
{
    public class UpdateResourceCommand : ICommand
    {
        public int ResourceId { get; }
        public int LocaleId { get; }
        public string ResourceKey { get; }
        public string ResourceSet { get; }
        public string Value { get; }

        public UpdateResourceCommand(int resourceId, int localeId, string resourceKey, string resourceSet, string value)
        {
            ResourceId = resourceId;
            LocaleId = localeId;
            ResourceKey = resourceKey;
            ResourceSet = resourceSet;
            Value = value;
        }
    }
}