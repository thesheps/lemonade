using System;
using Lemonade;
using Lemonade.Resolvers;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Feature.Switches["MyNewFeature1"])
            {
                Console.WriteLine("Hello World");
            }
        }
    }
}
