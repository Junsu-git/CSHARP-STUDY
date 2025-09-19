using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chap7.Overriding
{
    public class Overrideing
    {
        public static void Run()
        {
            Console.WriteLine("\nCreating ArmorSuite...");

            ArmorSuite armorsuite = new ArmorSuite();

            armorsuite.Initialize();

            Console.WriteLine("\nCreating IronMan...");
            ArmorSuite ironman = new IronMan();
            ironman.Initialize();

            Console.WriteLine("\nCreating WarMachine...");
            ArmorSuite warmachine = new Warmachine();
            warmachine.Initialize();

            Console.WriteLine();
        }
    }
}
