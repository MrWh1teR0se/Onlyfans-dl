using System;
using OnlyFansDL.Core;

namespace OnlyFansDL
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Core.Main main = new Main();
            main.Start().GetAwaiter().GetResult();
            Console.WriteLine("==== DONE! - enjoy ur content =====");
        }
        
    }
}