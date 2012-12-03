using System;
using System.Threading;
using ConsoleToolBox;

namespace ConsoleToolBoxDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            CtbManager ctbManager = CtbManager.Instance;
            ctbManager.Initialize();

            try
            {


                Console.WriteLine("Hello, world!");

            }
            catch
            {
                // do nothing
            }

            ctbManager.Cleanup();


               
#if DEBUG
            Console.Write("Press any key to continue...");
            Console.ReadKey();
#endif
        }
    }
}
