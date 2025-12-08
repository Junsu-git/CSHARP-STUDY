using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace csharp_009_task
{
    internal class Q1
    {
        // --- 'lock' 없이 스레드별 결과를 안전하게 저장하기 위한 공간 ---

        // 계산할 총 숫자와 스레드 개수
        const Int64 maxNum = 100_000_000_000; // 천억
        const int threadCount = 16;// Environment.ProcessorCount와 동일하게 설정


        // 각 스레드가 계산한 '부분 합계'를 저장할 배열
        // 각 스레드는 자신의 'index'에만 값을 쓰므로 'lock'이 필요 없음
        static Int128[] partialSums = new Int128[threadCount];
        static Int128 totalSum = 0;

        public void Run()
        {
            // 각 함수를 순차적으로 실행할 thread list
            List<Thread> threads1 = new List<Thread>(); // 1번 함수(I/O)용
            List<Thread> threads2 = new List<Thread>(); // 2번 함수(CPU)용
            Stopwatch st1 = new Stopwatch();
            Stopwatch st2 = new Stopwatch();

            // 각 스레드가 계산할 작업량 (범위)
            // (maxNum / threadCount)
            Int64 chunkSize = maxNum / threadCount;

            // --- 1. I/O 작업이 포함된 함수(reportThreadIDinTerminal) 테스트 ---
            Console.WriteLine($"--- 1. [I/O 포함] {threadCount}개 스레드로 분할 계산 시작 ---");
            Console.WriteLine("... (I/O 작업으로 인해 매우 오래 걸리며, 터미널 출력이 겹칠 수 있습니다) ...");
            ResetPartialSums(); // 합계 배열 초기화
            st1.Start();

            for (int i = 0; i < threadCount; i++)
            {
                // 각 스레드의 시작과 끝 범위 계산
                Int64 start = (i * chunkSize) + 1;
                // 마지막 스레드는 나머지 숫자를 모두 담당 (나누어 떨어지지 않을 경우 대비)
                Int64 end = (i == threadCount - 1) ? maxNum : (i + 1) * chunkSize;

                // 스레드에 여러 매개변수(자신의 인덱스, 시작값, 끝값)를 넘기기 위해 튜플(Tuple) 사용
                Thread t = new Thread(reportThreadIDinTerminal);
                threads1.Add(t);
                t.Start((i, start, end)); // (index, start, end) 전달
            }

            foreach (var thread in threads1)
            {
                thread.Join(); // 스레드가 끝날 때까지 대기
            }
            st1.Stop();

            // 메인 스레드가 각 스레드의 결과(부분 합계)를 더함
            for (int i = 0; i < threadCount; i++)
            {
                totalSum += partialSums[i];
            }
            Console.WriteLine("--- 1. [I/O 포함] 계산 완료 ---");
            Console.WriteLine($"[WATCH 1] = {st1.Elapsed} | Sum = {totalSum}");
            Console.WriteLine();


            // --- 2. CPU 연산만 있는 함수(calculateHugeSum) 테스트 ---
            Console.WriteLine($"--- 2. [CPU Only] {threadCount}개 스레드로 분할 계산 시작 ---");
            ResetPartialSums(); // 합계 배열 초기화
            threads2.Clear();   // 스레드 리스트 초기화
            st2.Start();

            for (int i = 0; i < threadCount; i++)
            {
                Int64 start = (i * chunkSize) + 1;
                Int64 end = (i == threadCount - 1) ? maxNum : (i + 1) * chunkSize;

                Thread t = new Thread(calculateHugeSum);
                threads2.Add(t);
                t.Start((i, start, end)); // (index, start, end) 전달
            }

            foreach (var thread in threads2)
            {
                thread.Join();
            }
            st2.Stop();

            totalSum = 0; // 합계 변수 초기화
            for (int i = 0; i < threadCount; i++)
            {
                totalSum += partialSums[i];
            }
            Console.WriteLine("--- 2. [CPU Only] 계산 완료 ---");
            Console.WriteLine($"[WATCH 2] = {st2.Elapsed} | Sum = {totalSum}");
            Console.WriteLine();

            // --- 결과 검증 ---
            // 1부터 n까지의 합 공식: n * (n + 1) / 2
            Int128 expectedSum = (Int128)maxNum * (maxNum + 1) / 2;
            Console.WriteLine($"Expected Sum:   {expectedSum}");
            Console.WriteLine($"Correct: {totalSum == expectedSum}");
            Console.WriteLine();

            /* 결론
             * 1번 함수의 경우, cpu 사용률이 최대 40퍼센트를 넘기 힘들다
             * 다만 cpu가 대기하는 시간이 길어지기 때문에, stopwatch에 찍히는 시간은 훨씬 더 길다.
             * 이유 : cpu에서 입출력 채널로의 통신이, only cpu 연산보다 훨씬 느리기 때문
             * * 2번 함수의 경우, cpu 사용률이 항시 100퍼센트에 육박한다.
             * 그러나, 입출력을 통한 대기시간이 없기 때문에 , stopwatch에 찍히는 시간은 훨씬 더 짧다.
             * 이유 : 입출력 채널을 거치는 대기시간을 거치지 않고, 오로지 cpu 연산만 수행하기 때문
             */
        }

        // 스레드 시작 시 합계 배열을 초기화하는 도우미 함수
        private void ResetPartialSums()
        {
            totalSum = 0;
            for (int i = 0; i < threadCount; i++)
            {
                partialSums[i] = 0;
            }
        }


        // 천억까지 합산 -> id 출력을 반복 (수정됨: 분할 계산)
        // [주의] 이 함수는 어마어마한 I/O 작업으로 인해 calculateHugeSum보다 
        // 비교도 안 될 만큼 *극도로* 느립니다. (실행에 몇 시간 또는 며칠이 걸릴 수 있음)
        public void reportThreadIDinTerminal(object? state)
        {
            if (state == null) return;
            // 전달받은 매개변수(인덱스, 시작값, 끝값)를 튜플로 변환
            (int index, Int64 start, Int64 end) = ((int, Int64, Int64))state;

            Int128 localSum = 0; // 스레드별 '로컬' 합계 변수 (***중요***)

            for (Int64 i = start; i <= end; i++)
            {
                localSum += i;
                // [주의] Console.WriteLine은 매우 느린 I/O 작업입니다.
                // 이 라인 때문에 스레드가 대부분의 시간을 '대기' 상태로 보냅니다.
                if(i % 10000 == 0) // 1천만 단위로 출력 빈도 줄임
                    Console.WriteLine($"[{i}]      [Thread {Thread.CurrentThread.ManagedThreadId} | Index {index}] ");
            }

            // 계산이 모두 끝난 후, 자신의 '칸'에만 결과값을 씀
            partialSums[index] = localSum;
        }

        // 천억까지 합산만 함 (수정됨: 분할 계산)
        public void calculateHugeSum(object? state)
        {
            if (state == null) return;
            // 전달받은 매개변수(인덱스, 시작값, 끝값)를 튜플로 변환
            (int index, Int64 start, Int64 end) = ((int, Int64, Int64))state;

            Int128 localSum = 0; // 스레드별 '로컬' 합계 변수

            for (Int64 i = start; i <= end; i++)
            {
                localSum += i;
            }

            // 계산이 모두 끝난 후, 자신의 '칸'에만 결과값을 씀
            partialSums[index] = localSum;

            Console.WriteLine($"[Thread {Thread.CurrentThread.ManagedThreadId} | Index {index}] Finished range {start}~{end}.");
        }
    }
}