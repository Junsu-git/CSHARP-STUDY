using System.Diagnostics;
using System.Threading;

namespace charp_008_Thread
{
    internal class Program
    {
        class myBag
        {
            public int data;
            public string str;
        }
        static void myCalculater(object data)
        {
            myBag myBag = (myBag)data;
            int sum = myBag.data;
            sum += 10;
            Console.WriteLine($"[Thread 2] 번호 []");
            // 큰 계싼이 필요해... 이 결과가 더 빨리 필요해...
            Thread.Sleep(8000); // 큰 계산이 있는 것처럼 표현한 것 뿐
            Console.WriteLine($"[Thread 2] 첫번째 스트링 [{myBag.str}]");
            //Thread.Sleep(1000); // 큰 계산이 있는 것처럼 표현한 것 뿐
            Console.WriteLine($"[Thread 2] 조준수 [{sum}]");
        }

        static void Main(string[] args)
        {
            myBag mb = new myBag();
            mb.data = 49;
            mb.str = "TEST STRING";

            Thread myThread = new Thread(myCalculater);
            //Thread myThread2 = new Thread(myCalculater);
            myThread.IsBackground = false;
            myThread.Start(mb);
            //myThread2.Start(mb);

            Int128 sum = 0;
            Console.WriteLine($"[Thread 1] 번호 [{myThread.ManagedThreadId}]");
            // ctrl + alt + h0
            Console.WriteLine($"[Thread 1] 매우 큰 계산 전");
            //for(Int64 i =0; i < 1_000_000_000; i++)
            //{
            //    sum += (Int128)i;
            //}
            Thread.Sleep(5_000);
            Console.WriteLine($"RESULT = [ {sum} ]");

            //Console.ReadLine();
        }
    }
}
