namespace Lemonade
{
    public static class Config
    {
        public static T Settings<T>(string key) => new ConfigValue<T>()[key];

        private class ConfigValue<T> : Value<T>
        {
            protected override T GetValue(string key, string applicationName)
            {
                return Lemonade.ConfigurationResolver.Resolve<T>(key, applicationName);
            }

            protected override string ValueType => "Configuration";
        }
    }
}