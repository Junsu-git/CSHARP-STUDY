using System.ComponentModel.Design;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ObjectiveC;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace chsarp_009_Thread3
{
    internal class Program
    {
        static UInt128 result = 0;
        static void Main(string[] args)
        {
            Stopwatch stopwatch;
            List<Thread> threads = new List<Thread>();
            
            stopwatch = new Stopwatch();
            stopwatch.Start();


            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                threads.Add(new Thread(Calc));
            }
            Console.WriteLine($"Thread Count = {threads.Count}");

            int n = 0;
            foreach (var thread in threads)
            {
                thread.Start(n++);
                thread.IsBackground = false;
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
            stopwatch.Stop();
            Console.WriteLine($"\n결과 : {result} 쓰레드 사용시간 : {stopwatch.Elapsed}");
        }

        public static void Calc(object v)
        {
            UInt64 flag = Convert.ToUInt64(v); // 문제 없음
            UInt64 n = (UInt64)10_000_000_000 / 20 * flag + 1;
            UInt64 sum = 0;

            for (UInt64 idx = 0; idx < 500_000_000; idx++)
            {
                sum += n++;
            }
            result += sum;
            //Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId} Thread] 종료");
            //Console.WriteLine($"[Thread] Result = [{sum}]");
        }
    }
}
