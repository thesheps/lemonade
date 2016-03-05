namespace Lemonade.Web.Core.Commands
{
    public class UpdateResourceCommand : ICommand
    {
        public int ResourceId { get; set; }
        public string Locale { get; set; }
        public string ResourceKey { get; set; }
        public string ResourceSet { get; set; }
        public string Value { get; set; }

        public UpdateResourceCommand(int resourceId, string locale, string resourceKey, string resourceSet, string value)
        {
            ResourceId = resourceId;
            Locale = locale;
            ResourceKey = resourceKey;
            ResourceSet = resourceSet;
            Value = value;
        }
    }
}