using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_009_task
{
    internal class Q5
    {
        public void Run()
        {
            Console.WriteLine("Q5 실행");
            ThreadSum ts = new ThreadSum(54542221335);
            ts.StartCalc(Environment.ProcessorCount);
        }
    }
}