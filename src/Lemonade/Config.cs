namespace Lemonade
{
    public class Config
    {
        public static T Settings<T>(string configurationName)
        {
            var key = "Config" + Lemonade.ApplicationName + configurationName;
            var value = Lemonade.CacheProvider
                .GetValue(key, () => Lemonade.ConfigurationResolver.Resolve<T>(configurationName, Lemonade.ApplicationName));

            return value;
        }
    }
}