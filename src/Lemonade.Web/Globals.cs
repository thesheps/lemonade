using System.Configuration;

namespace Lemonade.Web
{
    public class Globals
    {
        public static string ApplicationTitle => ConfigurationManager.AppSettings["ApplicationTitle"];
    }
}