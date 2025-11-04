using System.ComponentModel.Design;
using System.Runtime.InteropServices;

namespace csharp_010_Thread
{
    internal class Program
    {
        private static bool _running = false;
        private static bool _cancleRequest = false;


        static void Main(string[] args)
        {
            //List<int> radiusList = new List<int>();
            Queue<int> radiusQueue = new Queue<int>();
            // Caution Queue Thread = unsafe
            for (int i = 0; i < 65500; i++)
            {
                radiusQueue.Enqueue(i);
            }
            Thread t2 = new Thread(Run);
            ThreadEnable();
            t2.Start(radiusQueue);
            Console.WriteLine("Waiting Work Thread cal");
            Thread.Sleep(2000);

            Console.WriteLine("Enqueue New Data to Work Thread");
            for (int i = 65501; i <= 70000; i++)
            {
                radiusQueue.Enqueue(i);
            }
            Console.WriteLine("Waiting Work Thread Last update data cal");
            Console.WriteLine("============= Aborting Work Thread =============");

            Thread.Sleep(50);
            Console.WriteLine("============= Disable Work Thread =============");
            ThreadDisable();
            Console.WriteLine("============= Cancel Work Thread =============");
            DoThreadCancel();

            Console.WriteLine("[ MAIN ]Waiting Aborting work Thread in Main");
        }

        private static void ThreadEnable()
        {
            _running = true;
        }
        private static void ThreadDisable()
        {
            _running = false;
        }
        private static void DoThreadCancel()
        {
            _cancleRequest = true;
        }
        private static void ClearThreadCancel()
        {
            _cancleRequest = false;
        }
        private static bool IsThreadCancel()
        {
            return _cancleRequest;
        }

        static void Run(object o)
        {
            //List<int> list = (List<int>)o;
            Queue<int> q = (Queue<int>)o;
            Queue<double> rq = new Queue<double>();
            //List<double> resultList = new List<double>();

            double result = 0;
            try
            {
                while (_running)
                {
                    if (IsThreadCancel())
                    {
                        throw new Exception("USER REQUET CANCELLATION");
                    }
                    if (q.Count > 0)
                    {
                        // huge calc
                        //foreach (int i in q)
                        while (q.Count > 0)
                        {
                            if (IsThreadCancel())
                            {
                                throw new Exception("USER REQUET CANCELLATION");
                            }
                            int i = q.Dequeue();
                            result = i * i * Math.PI;
                            rq.Enqueue(result);
                        }
                        // report move to near by Thread.Sleep()

                    }
                    else
                    {
                        int idx = 0;
                        while (rq.Count > 0)
                        {
                            if (IsThreadCancel())
                            {
                                throw new Exception("USER REQUET CANCELLATION");
                            }
                            double d = rq.Dequeue();
                            Console.WriteLine($"[{idx++}] result = {d}");
                        }
                        Thread.Sleep(1);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
