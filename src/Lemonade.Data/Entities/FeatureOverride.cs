namespace Lemonade.Data.Entities
{
    public class FeatureOverride
    {
        public int FeatureOverrideId { get; set; }
        public int FeatureId { get; set; }
        public string Hostname { get; set; }
        public bool IsEnabled { get; set; }
    }
}