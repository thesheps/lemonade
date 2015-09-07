using System.Configuration;

namespace Lemonade
{
    public interface IFeatureResolver
    {
        bool Get(string value);
    }

    public class AppConfigFeatureResolver : IFeatureResolver
    {
        public bool Get(string value)
        {
            bool enabled;
            return bool.TryParse(ConfigurationManager.AppSettings[value], out enabled) && enabled;
        }
    }
}