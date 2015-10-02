using System;
using Lemonade;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Feature.Switches["MyNewFeature"])
            {
                Console.WriteLine("Test");
            }
        }
    }
}
