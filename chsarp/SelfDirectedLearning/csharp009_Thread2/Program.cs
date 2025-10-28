using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.ObjectiveC;

namespace csharp009_Thread2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 복습

            // 엄청 큰 계산 작업, 10초
            // 실제 빨리 해줘야하는 특별한 작업을 10초동안 기다려야함
            // 동시에 실행하기 위한 형태

            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                threads.Add(new Thread(Run));
            }
            Console.WriteLine($"Thread Count = {threads.Count}");
            foreach(Thread thread in threads)
            {
                thread.Start(10000);
            }


            Int64 sum = 0;
            // Thread 객체를 시작
            // 1. Thread.Sleep(10_000);
            //Thread.Sleep(5_000);
            for (Int64 i = 1; i<= 1_000_000_000; i++)
            {
                sum += 1;
            }
            // 2. 중요 작업 -> 함수로 묶어서

            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId} Main] 종료");
        }

        public static void myWork(object v)
        {
            myDataClss md = (myDataClss)v;
            //myDataClss md2 = v as myDataClss; // 추후에 캐스팅 관련 문제가 생길 때, 한번 확인하기
            int result1 = md.x;
            int result2 = md.y;
            int result = result1+result2;
            Thread.Sleep(3000);

            
            Console.WriteLine($"[Thread] Result = [{result}]");
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId} Thread] 종료");
        }

        public static void Run(object v)
        {
            int result = GetFibo((int)v);

            Console.WriteLine($"[Thread] Result = [{result}]");
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId} Thread] 종료");
        }

        public static int GetFibo(int v)
        {
            if ((int)v == 0) return 0;
            else if ((int)v <= 1) return 1;

            return GetFibo((int)v - 1) + GetFibo((int)v - 2);
        }
    }

    public class myDataClss
    {
        public int x;
        public int y;
        public int z;
    }
}
