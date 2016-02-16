namespace Lemonade
{
    public static class Config
    {
        public static T Settings<T>(string key) => new ConfigValue<T>()[key];

        private class ConfigValue<T> : ValueProvider<T>
        {
            protected override T GetValue(string key, string applicationName)
            {
                return Configuration.ConfigurationResolver.Resolve<T>(key, applicationName);
            }

            protected override string ValueType => "Configuration";
        }
    }
}