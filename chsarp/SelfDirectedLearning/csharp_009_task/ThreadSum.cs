using System;
using System.Diagnostics;

namespace csharp_009_task
{
    internal class ThreadSum
    {
        TMemory tm;
        List<Thread> threads = new List<Thread>();
        Stopwatch sw = new Stopwatch();
        public ThreadSum(UInt128 v)
        {
            Init(v);
        }
        void Init(UInt128 v)
        {
            tm = new TMemory();
            tm.MaxNum = v;
        }

        public void StartCalc(int n)
        {
            tm.PartitionSum = new UInt128[n];
            InitThread(n);
            StopWatchStart();
            StartThread();
            JoinThread();
            StopWatchEnd();
            PrintResult();
        }

        private void StopWatchEnd()
        {
            Console.WriteLine($"[NOTICE] STOPWATCH STOP");
            sw.Stop();
        }

        private void StopWatchStart()
        {
            Console.WriteLine($"[NOTICE] STOPWATCH START");
            sw.Start();
        }

        private void InitThread(int n)
        {
            Console.WriteLine($"[NOTICE] No of {n} Threads Create");
            for (int i = 0; i < n; i++)
            {
                Thread t = new Thread(Calculation);
                threads.Add(t);
            }
            tm.Range = tm.MaxNum / (UInt128)n;
        }

        private void StartThread()
        {
            Console.WriteLine($"[NOTICE] Operate Threads");
            UInt128 sNum = 1; // ⭐️ 첫 시작 번호는 1로 초기화
            UInt128 eNum;

            for (int idx = 0; idx < threads.Count; idx++)
            {
                eNum = (idx == threads.Count - 1) ? tm.MaxNum : sNum + tm.Range - 1;

                var args = (idx, sNum, eNum);
                threads[idx].Start(args);

                sNum = eNum + 1;
            }
        }
        private void JoinThread()
        {
            for(int Idx = 0; Idx < threads.Count; Idx++)
            {
                threads[Idx].Join();
            }
        }

        private void Calculation(object? state)
        {
            if (state == null) { return; }
            (int idx, UInt128 sNum, UInt128 eNum) = ((int, UInt128, UInt128))state;
            for(UInt128 i = sNum; i <= eNum; i++)
            {
                tm.PartitionSum[idx] += i;
            }
            Console.WriteLine($"[THREAD {threads[idx].ManagedThreadId}] Range : {sNum} ~ {eNum} Finished");
            tm.Result += tm.PartitionSum[idx];
        }
        private void PrintResult()
        {
            Console.WriteLine($"[RESULT]\nUser Value : {tm.MaxNum}");
            Console.WriteLine($"Created Thread number : {threads.Count}");
            Console.WriteLine($"Calculated Sum : {tm.Result}");
            Console.WriteLine($"Elapsed Time : {sw.Elapsed}");
        }
    }
}