namespace csharp_010_ThreadPool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for(int i = 0; i < 65_500; i++)
            {
                //Thread t1 = new Thread(Run);
                //t1.Start(i);
                ThreadPool.QueueUserWorkItem(Run, i);
            }

            // main 종료되는 것을 방지, Cause Thread pool is Background
            Console.ReadLine();
        }

        static void Run(object obj)
        {
            int r = (int)obj;
            double result = r * r * Math.PI;
            Console.WriteLine($"[ {Thread.CurrentThread.ManagedThreadId} ] radius: {r} result: {result}"); // 예제 편의상 쓰레드 내부에서 출력
        }

        // 경마 
        // 청소 루틴
        // 로그 게임
        // 
    }
}
