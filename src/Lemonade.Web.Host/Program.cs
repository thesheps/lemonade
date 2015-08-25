using System;

namespace Lemonade.Web.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var startup = new StartUp("http://localhost:1048"))
            {
                startup.Start();
                Console.ReadKey();
            }
        }
    }
}