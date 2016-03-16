namespace Lemonade.Web.Contracts
{
    public class GenerateResources
    {
        public int ApplicationId { get; set; }
        public int LocaleId { get; set; }
        public int TargetLocaleId { get; set; }
        public string Type { get; set; }
    }
}