namespace Lemonade
{
    public class Config
    {
        public static T Settings<T>(string key)
        {
            return Lemonade.ConfigurationResolver.Resolve<T>(key, Lemonade.ApplicationName);
        }
    }
}